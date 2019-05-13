using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public struct SortedAnalyticStorage
{
	public GameObject raycastTrackerObject;
	public List<List<Analytic>> roomVisits;

	public SortedAnalyticStorage(GameObject room)
	{
		raycastTrackerObject = room;
		roomVisits = new List<List<Analytic>>();
	}

	//public void Initialise()
	//{
	//	roomVisits = new List<List<Analytic>>();
	//}

	public void Clear()
	{
		foreach (List<Analytic> la in roomVisits)
		{
			la.Clear();
		}
	}
}

public class RenderViewData : MonoBehaviour {

	[SerializeField] private Analytics analytics;

	//references to the rooms.
	[SerializeField] public List<GameObject> raycastTrackerObjects;

	public List<SortedAnalyticStorage> roomsInfo;
	
	public float timeCodeInterval = 1;

	//This needs to be a reference, since if you attempt to create the material programmatically,
	//Unity fails to pass it into the android build.
	[SerializeField] private Material matTest;
	[SerializeField] private Material transparentMaterial;
	[SerializeField] private Material heatMapMaterial;

	[SerializeField] private GameObject pointPrefab;
	//private SceneController sceneController;
	private List<GameObject> lineRenders;
	
	
	private void Start()
	{
		//analytics.clearData();

		//sceneController = GetComponent<SceneController>();
		lineRenders = new List<GameObject>();

		//initialise the sorted array for assigning to later.
		//raycastAnalyticSorted = new List<List<List<Analytic>>>();
		roomsInfo = new List<SortedAnalyticStorage>();

		for (int i = 0; i < raycastTrackerObjects.Count; i++)
		{
			roomsInfo.Add(new SortedAnalyticStorage(raycastTrackerObjects[i]));
		}

		analytics.ClearData();
	}

	public void refreshAnalytics()
	{
		//clear the rooms.
		if (roomsInfo.Count > 0)
		{
			//for each room.
			foreach (SortedAnalyticStorage SAS in roomsInfo)
			{
				SAS.Clear();
			}
		}



		if (lineRenders.Count > 0)
		{
			foreach (GameObject lr in lineRenders)
			{
				Destroy(lr);
			}
			lineRenders.Clear();
		}
		if (raycastTrackerObjects.Count > 0)
		{
			foreach (GameObject RaycastTracker in raycastTrackerObjects)
			{
				RaycastTracker.GetComponent<MeshFilter>().mesh.colors = null;
			}
		}


		Debug.Log("ShowViewPath Begin");
		if (analytics.getCount() > 0)
		{
			//string previousRoomName = "";
			//GameObject previousRoom = null;
			//SortedAnalyticStorage prevSAS = roomsInfo[0];
			int prevIterator = 0;

			List<Analytic> visitList = new List<Analytic>();

			//sort the analytics according to their rooms.
			//for each analytic
			foreach (Analytic analytic in analytics.analyticStorage.visionTrackingData)
			{

				
				//for each room.
				//foreach (SortedAnalyticStorage sas in roomsInfo)
				for (int i = 0; i < roomsInfo.Count; i++)
				{
					if (analytic.Room == roomsInfo[i].raycastTrackerObject.name)
					{
						//if its a different visit
						if (roomsInfo[prevIterator].raycastTrackerObject.name != analytic.Room)
						{
							//add everything to the last room
							roomsInfo[prevIterator].roomVisits.Add(visitList);
							//visitList.Clear();
							visitList = new List<Analytic>();

							visitList.Add(analytic);

							//set current room as previous
							//prevSAS = sas;
							prevIterator = i;
						}
						else //else if the point is in the same room
						{
							visitList.Add(analytic);
						}

						continue;
					}
				}
			}
			roomsInfo[prevIterator].roomVisits.Add(visitList);


			CreateLineRender();
			CreateHeatMap();
			DisableAllMetrics();
			ShowLineRender();
		}
		else
		{
			Debug.Log("Analytics don't exist");
		}
		
	}


	private void CreateLineRender()
	{

		//for each room
		foreach (SortedAnalyticStorage ri in roomsInfo)
		{

			//if the room has visits assigned to it.
			if (ri.roomVisits.Count > 0)
			{

				//for each visit.
				foreach (List<Analytic> lana in ri.roomVisits)
				{

					if (lana.Count > 0)
					{

						GameObject connectionGO;
						connectionGO = new GameObject(lana[0].Room + " Connections");

						//setup line renderer
						LineRenderer lr = connectionGO.AddComponent<LineRenderer>();
						lr.startWidth = 0.2f;
						lr.endWidth = 0.2f;
						lr.useWorldSpace = true;
						lr.positionCount = lana.Count;
						lr.material = matTest;
						lr.startColor = Color.green;
						lr.endColor = Color.red;

						float previousTimeCode = 0;

						//for each analytic in the visit.
						for (int i = 0; i < lana.Count; i++)
						{
							//add a point on the line renderer
							lr.SetPosition(i, lana[i].Point);

							//only show every second.
							if (lana[i].TimeStamp > previousTimeCode + timeCodeInterval)
							{

								//Instantiate the point.
								GameObject pointText = Instantiate(pointPrefab);
								TimePointScript tps = pointText.GetComponent<TimePointScript>();
								tps.OnCreatePoint(lana[i].Point, connectionGO.transform, lana[i].TimeStamp);
								previousTimeCode = lana[i].TimeStamp;
							}
						}

						//set the parent for the line renderer
						connectionGO.transform.parent = ri.raycastTrackerObject.transform;
						lineRenders.Add(connectionGO);
					}
				}				
			}
		}
	}

	private void CreateHeatMap()
	{
		foreach (SortedAnalyticStorage sas in roomsInfo)
		{
			HeatMap heatMap = sas.raycastTrackerObject.GetComponent<HeatMap>();

			foreach (List<Analytic> la in sas.roomVisits)
			{
				heatMap.setData(la);
			}
		}
	}

	public void ShowHeatMap()
	{
		foreach (GameObject raycastObject in raycastTrackerObjects)
		{
			raycastObject.GetComponent<Renderer>().material = heatMapMaterial;
		}
	}

	public void ShowLineRender()
	{
		foreach (GameObject pointList in lineRenders)
		{
			pointList.SetActive(true);
		}
	}

	public void DisableAllMetrics()
	{
		foreach (GameObject pointList in lineRenders)
		{
			pointList.SetActive(false);
		}

		foreach (GameObject raycastObject in raycastTrackerObjects)
		{
			raycastObject.GetComponent<Renderer>().material = transparentMaterial;
		}
	}
}
