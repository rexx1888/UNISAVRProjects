using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMap : MonoBehaviour {

	private List<Analytic> roomAnalytics;
	[SerializeField] private Material RoomHeatmapMaterial;
	//[SerializeField] private Material ColdRoomMaterial;

	//[SerializeField] private Material[] materials;

	[SerializeField] Vector4 mapPointProperty;
	[SerializeField] public Vector4[] properties;
	[SerializeField] public Vector4[] positions;
	private int totalCalculatedPoints;


	// Use this for initialization
	void Start ()
	{
		roomAnalytics = new List<Analytic>();
		totalCalculatedPoints = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (roomAnalytics.Count > 0)
		{
			//for (int i = 0; i < roomAnalytics.Count; i++)
			//{
			//	properties[i] = mapPointProperty;
			//}

			RoomHeatmapMaterial.SetInt("_Points_Length", totalCalculatedPoints);
			RoomHeatmapMaterial.SetVectorArray("_Points", positions);
			RoomHeatmapMaterial.SetVectorArray("_Properties", properties);
			Debug.Log(properties.Length);

		}
	}

	public void setData(List<Analytic> givenAnalytics)
	{
		roomAnalytics = givenAnalytics;
		//List<Analytic> CalculatedPoints = new List<Analytic>();

		properties = new Vector4[roomAnalytics.Count];
		Debug.Log(properties.Length);
		positions = new Vector4[roomAnalytics.Count];

		

		//iterate through room specific data array
		for (int i = 0; i < roomAnalytics.Count; i++)
		{
			bool AddToList = true;
			//iterate through sorted data

			int TestAmount = 0;
			int testMax = 0;
			int testMin = 0;

			if (i > 10 && roomAnalytics.Count > i + 10)
			{
				testMin = i - 10;
				testMax = i + 10;
				//TestAmount = i - 10;
			}
			else if (i < 10 && roomAnalytics.Count > i + 10)
			{
				testMin = 0;
				testMax = i + 10;
			}
			else
			{
				testMax = roomAnalytics.Count;
			}


			for (int x = testMin; x < testMax; x++)
			{
				
				//if sorted data contains a point that is close to a pre existing point
				if (Vector3.Distance(positions[x], roomAnalytics[i].Point) < 1 && roomAnalytics[x].TimeStamp != roomAnalytics[i].TimeStamp)
				{
					properties[x] += mapPointProperty * 0.1f;
					AddToList = false;
				}
			}
			Debug.Log(properties.Length);
			if (AddToList == true)
			{
				properties[totalCalculatedPoints] = mapPointProperty;
				positions[totalCalculatedPoints] = new Vector4(roomAnalytics[i].Point.x, roomAnalytics[i].Point.y, roomAnalytics[i].Point.z);
				totalCalculatedPoints++;
			}
		}

		//Material[] mats = GetComponent<Renderer>().materials;

		GetComponent<Renderer>().material = RoomHeatmapMaterial;
	}
}
