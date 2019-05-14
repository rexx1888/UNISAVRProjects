using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public struct SortedAnalyticStorage
{
	[HideInInspector]
	public GameObject raycastTrackerObject;
	public List<List<Analytic>> roomVisits;

	public SortedAnalyticStorage(GameObject room)
	{
		raycastTrackerObject = room;
		roomVisits = new List<List<Analytic>>();
	}

	public void Clear()
	{
		foreach (List<Analytic> la in roomVisits)
		{
			la.Clear();
		}
	}
}

public class RenderViewData : MonoBehaviour {

	[Header("Analytics Scriptable object")]
	[SerializeField] private Analytics analytics;

	//references to the rooms.
	[HideInInspector]
	[SerializeField] public List<GameObject> raycastTrackerObjects;

	[Header("Time interval between points")]
	public float timeCodeInterval = 1;
	
	[Header("Heat map materials")]
	[SerializeField] private Material transparentMaterial;
	[SerializeField] private Material heatMapMaterial;

	[Header("View line point prefab")]
	[SerializeField] private GameObject pointPrefab;
	//This needs to be a reference, since if you attempt to create the material programmatically,
	//Unity fails to pass it into the android build.
	[SerializeField] private Material lineRendererMaterial;

	private List<GameObject> lineRenders;
	private List<SortedAnalyticStorage> sortedRoomAnalytics;



	private void Start()
	{
		//initialise the sorted array for assigning to later.
		sortedRoomAnalytics = new List<SortedAnalyticStorage>();
		lineRenders = new List<GameObject>();

		for (int i = 0; i < raycastTrackerObjects.Count; i++)
		{
			sortedRoomAnalytics.Add(new SortedAnalyticStorage(raycastTrackerObjects[i]));
		}

		analytics.ClearData();
	}

	public void refreshAnalytics()
	{
		clearLists();

		if (analytics.getCount() > 0)
		{
			int previousRoomIndex= 0;

			List<Analytic> visitList = new List<Analytic>();

			//sort the analytics according to their rooms.
			//for each analytic
			foreach (Analytic analytic in analytics.analyticStorage.visionTrackingData)
			{				
				//for each room.
				for (int i = 0; i < sortedRoomAnalytics.Count; i++)
				{
					if (analytic.Room == sortedRoomAnalytics[i].raycastTrackerObject.name)
					{
						//if its a different visit
						if (sortedRoomAnalytics[previousRoomIndex].raycastTrackerObject.name != analytic.Room)
						{
							//add everything to the last room
							if (visitList.Count > 0)
								sortedRoomAnalytics[previousRoomIndex].roomVisits.Add(visitList);
							
							//clear list
							visitList = new List<Analytic>();
							visitList.Add(analytic);

							//set current room as previous
							previousRoomIndex = i;
						}
						else //else if the point is in the same room
						{
							visitList.Add(analytic);
						}

						continue;
					}
				}
			}
			sortedRoomAnalytics[previousRoomIndex].roomVisits.Add(visitList);

			//create the analytic visualisations.
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

	private void clearLists()
	{
		//clear the rooms.
		if (sortedRoomAnalytics.Count > 0)
		{
			//for each room.
			foreach (SortedAnalyticStorage SAS in sortedRoomAnalytics)
			{
				SAS.Clear();
			}
		}

		//delete the line renderers.
		if (lineRenders.Count > 0)
		{
			foreach (GameObject lr in lineRenders)
			{
				Destroy(lr);
			}
			lineRenders.Clear();
		}

		//clear the heatmaps
		if (raycastTrackerObjects.Count > 0)
		{
			foreach (GameObject RaycastTracker in raycastTrackerObjects)
			{
				RaycastTracker.GetComponent<MeshFilter>().mesh.colors = null;
			}
		}
	}

	private void CreateLineRender()
	{

		//for each room
		foreach (SortedAnalyticStorage ri in sortedRoomAnalytics)
		{

			//if the room has visits assigned to it.
			if (ri.roomVisits.Count > 0)
			{

				//for each visit.
				foreach (List<Analytic> lana in ri.roomVisits)
				{
					//if the visit contains information.
					if (lana.Count > 0)
					{

						//create line renderer
						GameObject connectionGO;
						connectionGO = new GameObject(lana[0].Room + " Connections");

						LineRenderer lr = connectionGO.AddComponent<LineRenderer>();
						lr.startWidth = 0.2f;
						lr.endWidth = 0.2f;
						lr.useWorldSpace = true;
						lr.positionCount = lana.Count;
						lr.material = lineRendererMaterial;
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
		foreach (SortedAnalyticStorage sas in sortedRoomAnalytics)
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
		foreach (SortedAnalyticStorage sas in sortedRoomAnalytics)
		{
			if (sas.roomVisits.Count > 0)
				sas.raycastTrackerObject.GetComponent<Renderer>().material = heatMapMaterial;
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
