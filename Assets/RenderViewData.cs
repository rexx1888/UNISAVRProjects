using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;




public class RenderViewData : MonoBehaviour {

	[SerializeField] private Analytics analytics;

	//store the different rooms
	[SerializeField] private List<Transform> raycastTrackerObjects;
	private List<List<Analytic>> raycastAnalyticSorted;
	//The Rooms and the list of lists are stored the same, so say theres 3 rooms,
	//raycastAnalyticSorted[x] will store the list of analytics referencing raycastTrackerObjects[x]
	//the same index can be used between these arrays.

	[SerializeField] private GameObject pointPrefab;

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
					//lr.SetColors(Color.green, Color.red);
					lr.material = new Material(Shader.Find("Particles/Additive"));
					lr.startColor = Color.green;
					lr.endColor = Color.red;


					//Iterate through the analytics list.
					//if the data point's name is this room
					for (int i = 0; i < raycastAnalyticSorted[x].Count; i++)
					{
						Debug.Log("New point in " + raycastAnalyticSorted[x][i].Room);
						lr.SetPosition(i, raycastAnalyticSorted[x][i].Point);
						//Debug.Log(raycastAnalyticSorted[x][i].Point);
						//Debug.Log(raycastAnalyticSorted[x][i].Room);

						//Render each point along the line.
						//GameObject point = GameObject.CreatePrimitive(PrimitiveType.Sphere);
						////Instantiate(point, raycastAnalyticSorted[x][i].Point, Quaternion.identity);
						//point.transform.parent = raycastTrackerObjects[x];
						//point.transform.position = raycastAnalyticSorted[x][i].Point;

						////render the timecode of each point.
						//GameObject pointText = new GameObject("Text");


						//TextMeshPro textMesh = pointText.AddComponent<TextMeshPro>();
						//textMesh.color = Color.black;
						////format the seconds into time text
						//int minutes = Mathf.FloorToInt(raycastAnalyticSorted[x][i].TimeStamp / 60F);
						//int seconds = Mathf.FloorToInt(raycastAnalyticSorted[x][i].TimeStamp - minutes * 60);
						//string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);


						//textMesh.text = niceTime;

						////set the timeCode as a child (Since textmesh and Mesh Filter don't like eachother)
						//pointText.transform.SetParent(point.transform);
						//pointText.transform.localPosition = new Vector3(0, 5, 0);

						GameObject pointText = Instantiate(pointPrefab);
						TimePointScript tps = pointText.GetComponent<TimePointScript>();
						tps.OnCreatePoint(raycastAnalyticSorted[x][i].Point, raycastTrackerObjects[x], raycastAnalyticSorted[x][i].TimeStamp);

						//rotate the text to look at the camera
						//pointText.transform.LookAt(Camera.main.transform.position);
						//pointText.transform.rotation = Quaternion.LookRotation(-pointText.transform.forward);
					}

					//set the parent for the line renderer
					connectionGO.transform.parent = raycastTrackerObjects[x];
				}
			}			
		}
		else
		{
			Debug.Log("Analytics don't exist");
		}
	}
}
