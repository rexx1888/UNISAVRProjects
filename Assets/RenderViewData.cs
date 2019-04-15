using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderViewData : MonoBehaviour {

	[SerializeField] private Analytics analytics;
	//[SerializeField] private

	//store the different rooms
	[SerializeField] private List<Transform> raycastTrackerObjects;
	private List<List<Analytic>> raycastAnalyticSorted;

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
					}
				}
			}


			//iterate through the sorted array per room
			foreach (List<Analytic> analyticLists in raycastAnalyticSorted)
			{

				if (analyticLists.Count > 0)
				{
					GameObject connectionGO;
					connectionGO = new GameObject(transform.name + " Connections");

					//setup line renderer
					LineRenderer lr = connectionGO.AddComponent<LineRenderer>();
					lr.startWidth = 0.2f;
					lr.endWidth = 0.2f;
					lr.useWorldSpace = true;
					lr.positionCount = analyticLists.Count;


					//Iterate through the analytics list.
					//if the data point's name is this room
					for (int i = 0; i < analyticLists.Count; i++)
					{
						//if (analytics.analyticsStorage[i].Room == RaycastO.name)
						//{
						lr.SetPosition(i, analyticLists[i].Point);
						Debug.Log(analyticLists[i].Point);
						Debug.Log(analyticLists[i].Room);
						//}
					}

					//set the parent for the line renderer
					//connectionGO.transform.parent = raycastTrackerObjects.Find(object);
				}
			}

			
			
		}
	}
}
