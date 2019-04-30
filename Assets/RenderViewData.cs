using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;




public class RenderViewData : MonoBehaviour {

	[SerializeField] private Analytics analytics;

	//store the different rooms
	[SerializeField] public List<GameObject> raycastTrackerObjects;



	private List<List<Analytic>> raycastAnalyticSorted;
	//The Rooms and the list of lists are stored the same, so say theres 3 rooms,
	//raycastAnalyticSorted[x] will store the list of analytics referencing raycastTrackerObjects[x]
	//the same index can be used between these arrays.

	[SerializeField] private GameObject pointPrefab;

	//This needs to be a reference, since if you attempt to create the material programmatically,
	//Unity fails to pass it into the android build.
	[SerializeField] private Material matTest;


	private void Start()
	{
		//initialise the sorted array for assigning to later.
		raycastAnalyticSorted = new List<List<Analytic>>();
		for (int i = 0; i < raycastTrackerObjects.Count; i++ )
		{
			raycastAnalyticSorted.Add(new List<Analytic>());
		}

	}

	public void ShowViewPath()
	{
		Debug.Log("ShowViewPath Begin");
		if (analytics.getCount() > 0)
		{
			//sort the analytics according to their rooms.
			//iterate through the analytics list
			foreach (Analytic analytic in analytics.analyticsStorage)
			{
				//iterate through the rooms
				for (int i = 0; i < raycastTrackerObjects.Count; i++)
				{
					//if the analytic matches the room, store it in the sorted list
					if (analytic.Room == raycastTrackerObjects[i].name)
					{
						raycastAnalyticSorted[i].Add(analytic);

						continue;
					}
				}
			}

			//CreateLineRender);
			CreateHeatMap();
			
		}
		else
		{
			Debug.Log("Analytics don't exist");
		}
	}

	private void CreateLineRender()
	{
		//iterate through the sorted array per room
		for (int x = 0; x < raycastAnalyticSorted.Count; x++)
		{
			//for now we are using a line renderer to show how the user looks around the room.
			if (raycastAnalyticSorted[x].Count > 0)
			{
				GameObject connectionGO;
				connectionGO = new GameObject(raycastAnalyticSorted[x][0].Room + " Connections");

				//setup line renderer
				LineRenderer lr = connectionGO.AddComponent<LineRenderer>();
				lr.startWidth = 0.2f;
				lr.endWidth = 0.2f;
				lr.useWorldSpace = true;
				lr.positionCount = raycastAnalyticSorted[x].Count;
				lr.material = matTest;
				lr.startColor = Color.green;
				lr.endColor = Color.red;


				//Iterate through the analytics list.
				//if the data point's name is this room
				for (int i = 0; i < raycastAnalyticSorted[x].Count; i++)
				{
					//Debug.Log("New point in " + raycastAnalyticSorted[x][i].Room);
					//add a point on the line renderer
					lr.SetPosition(i, raycastAnalyticSorted[x][i].Point);

					//Instantiate the point.
					GameObject pointText = Instantiate(pointPrefab);
					TimePointScript tps = pointText.GetComponent<TimePointScript>();
					tps.OnCreatePoint(raycastAnalyticSorted[x][i].Point, raycastTrackerObjects[x].transform, raycastAnalyticSorted[x][i].TimeStamp);
				}

				//set the parent for the line renderer
				connectionGO.transform.parent = raycastTrackerObjects[x].transform;
			}
		}
	}

	private void CreateHeatMap()
	{
		//foreach (GameObject room in raycastTrackerObjects)
		for (int i = 0; i < raycastTrackerObjects.Count; i++)
		{
			HeatMap heatMap = raycastTrackerObjects[i].GetComponent<HeatMap>();
			heatMap.setData(raycastAnalyticSorted[i]);
		}
	}
}
