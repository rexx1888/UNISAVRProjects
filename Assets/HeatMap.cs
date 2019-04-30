using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMap : MonoBehaviour {

	private List<Analytic> roomAnalytics;
	[SerializeField] private Material RoomHeatmapMaterial;
	//[SerializeField] private Material ColdRoomMaterial;

	//[SerializeField] private Material[] materials;

	[SerializeField] Vector4 mapPointProperty;
	private Vector4[] properties;
	public Vector4[] positions;
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
			for (int i = 0; i < roomAnalytics.Count; i++)
			{
				properties[i] = mapPointProperty;
			}

			RoomHeatmapMaterial.SetInt("_Points_Length", properties.Length);
			RoomHeatmapMaterial.SetVectorArray("_Points", positions);
			RoomHeatmapMaterial.SetVectorArray("_Properties", properties);
		}
	}

	public void setData(List<Analytic> givenAnalytics)
	{
		roomAnalytics = givenAnalytics;
		//List<Analytic> CalculatedPoints = new List<Analytic>();

		properties = new Vector4[roomAnalytics.Count];
		positions = new Vector4[roomAnalytics.Count];

		

		//iterate through room specific data array
		for (int i = 0; i < roomAnalytics.Count; i++)
		{
			//iterate through sorted data
			for (int x = 0; x < roomAnalytics.Count; x++)
			{
				Debug.Log(Vector3.Distance(roomAnalytics[x].Point, roomAnalytics[i].Point));
				//if sorted data contains a point that is close to a pre existing point
				if (Vector3.Distance(roomAnalytics[x].Point, roomAnalytics[i].Point) < 10 && roomAnalytics[x].TimeStamp != roomAnalytics[i].TimeStamp)
				{
					properties[x] *= 2;
				}
				else
				{
					properties[i] = mapPointProperty;
					positions[i] = new Vector4(roomAnalytics[i].Point.x, roomAnalytics[i].Point.y, roomAnalytics[i].Point.z);
					totalCalculatedPoints++;
				}
			}			
		}

		//Material[] mats = GetComponent<Renderer>().materials;

		GetComponent<Renderer>().material = RoomHeatmapMaterial;
	}
}
