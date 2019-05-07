using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;




public class RenderViewData : MonoBehaviour {

	[SerializeField] private Analytics analytics;

	//store the different rooms
	[SerializeField] public List<GameObject> raycastTrackerObjects;

	//The Rooms and the list of lists are stored the same, so say theres 3 rooms,
	//raycastAnalyticSorted[x] will store the list of analytics referencing raycastTrackerObjects[x]
	//the same index can be used between these arrays.
	//private List<List<Analytic>> raycastAnalyticSorted;
	private List<List<List<Analytic>>> raycastAnalyticSorted;

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
		raycastAnalyticSorted = new List<List<List<Analytic>>>();

		//if (raycastAnalyticSorted != null)
		//{
		//	foreach (List<List<Analytic>> lla in raycastAnalyticSorted)
		//	{
		//		if (lla.Count > 0)
		//		{
		//			foreach (List<Analytic> la in lla)
		//			{
		//				la.Clear();
		//			}
		//		}
		//		lla.Clear();
		//	}
		//	raycastAnalyticSorted.Clear();
		//}
		analytics.ClearData();
	}

	public void refreshAnalytics()
	{
		if (raycastAnalyticSorted != null)
		{
			foreach (List<List<Analytic>> lla in raycastAnalyticSorted)
			{
				if (lla.Count > 0)
				{
					foreach (List<Analytic> la in lla)
					{
						la.Clear();
					}
				}
				lla.Clear();
			}
			raycastAnalyticSorted.Clear();
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

		//initialise the sorted array for assigning to later.
		for (int i = 0; i < raycastTrackerObjects.Count; i++)
		{
			raycastAnalyticSorted.Add(new List<List<Analytic>>());
		}

		Debug.Log("ShowViewPath Begin");
		if (analytics.getCount() > 0)
		{
			string previousRoomName = "";
			List<Analytic> visitList = new List<Analytic>();

			//sort the analytics according to their rooms.
			//iterate through the analytics list
			foreach (Analytic analytic in analytics.analyticStorage.visionTrackingData)
			{
				
				//iterate through the rooms
				for (int i = 0; i < raycastTrackerObjects.Count; i++)
				{
					//if the analytic matches the room, store it in the sorted list
					if (analytic.Room == raycastTrackerObjects[i].name)
					{
						if (previousRoomName != analytic.Room) //if the user went to a new room
						{
							List<Analytic> newL = new List<Analytic>();
							foreach(Analytic a in visitList)
							{
								newL.Add(a);
							}
							raycastAnalyticSorted[i].Add(visitList);
							visitList.Clear();
							//visitList = new List<Analytic>();

							visitList.Add(analytic);
							previousRoomName = analytic.Room;
						}
						else //else if the point is in the same room
						{
							visitList.Add(analytic);
						}

						continue;
					}
				}
			}

			CreateLineRender();
			CreateHeatMap();
			DisableAllMetrics();
			//ShowLineRender();
		}
		else
		{
			Debug.Log("Analytics don't exist");
		}
	}


	private void CreateLineRender()
	{
		
		//iterate through rooms
		for (int x = 0; x < raycastAnalyticSorted.Count; x++)
		{

			//for now we are using a line renderer to show how the user looks around the room.
			if (raycastAnalyticSorted[x].Count > 0)
			{
				for (int z = 0; z < raycastAnalyticSorted[x].Count; z++)
				{
					GameObject connectionGO;
					connectionGO = new GameObject(raycastAnalyticSorted[x][z][0].Room + " Connections");

					//setup line renderer
					LineRenderer lr = connectionGO.AddComponent<LineRenderer>();
					lr.startWidth = 0.2f;
					lr.endWidth = 0.2f;
					lr.useWorldSpace = true;
					lr.positionCount = raycastAnalyticSorted[x][z].Count;
					lr.material = matTest;
					lr.startColor = Color.green;
					lr.endColor = Color.red;

					float previousTimeCode = 0;

					//Iterate through the analytics list.
					//if the data point's name is this room
					for (int i = 0; i < raycastAnalyticSorted[x][z].Count; i++)
					{


						//add a point on the line renderer
						lr.SetPosition(i, raycastAnalyticSorted[x][z][i].Point);

						if (raycastAnalyticSorted[x][z][i].TimeStamp > previousTimeCode + timeCodeInterval)
						{

							//Instantiate the point.
							GameObject pointText = Instantiate(pointPrefab);
							TimePointScript tps = pointText.GetComponent<TimePointScript>();


							tps.OnCreatePoint(raycastAnalyticSorted[x][z][i].Point, connectionGO.transform, raycastAnalyticSorted[x][z][i].TimeStamp);
							previousTimeCode = raycastAnalyticSorted[x][z][i].TimeStamp;
						}
					}

					//set the parent for the line renderer
					connectionGO.transform.parent = raycastTrackerObjects[x].transform;
					lineRenders.Add(connectionGO);
				}				
			}
		}
	}

	private void CreateHeatMap()
	{
		//foreach room
		for (int i = 0; i < raycastTrackerObjects.Count; i++)
		{
			HeatMap heatMap = raycastTrackerObjects[i].GetComponent<HeatMap>();
			for (int x = 0; x < raycastAnalyticSorted[i].Count; x++)
			{
				heatMap.setData(raycastAnalyticSorted[i][x]);
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
