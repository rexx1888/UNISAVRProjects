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



	// Use this for initialization
	void Start ()
	{
		roomAnalytics = new List<Analytic>();
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

			RoomHeatmapMaterial.SetInt("_Points_Length", roomAnalytics.Count);
			RoomHeatmapMaterial.SetVectorArray("_Points", positions);
			RoomHeatmapMaterial.SetVectorArray("_Properties", properties);
		}
	}

	public void setData(List<Analytic> givenAnalytics)
	{
		roomAnalytics = givenAnalytics;

		properties = new Vector4[roomAnalytics.Count];
		positions = new Vector4[roomAnalytics.Count];

		for (int i = 0; i < roomAnalytics.Count; i++)
		{
			properties[i] = mapPointProperty;

			positions[i] = new Vector4(roomAnalytics[i].Point.x, roomAnalytics[i].Point.y, roomAnalytics[i].Point.z);
		}

		//Material[] mats = GetComponent<Renderer>().materials;

		GetComponent<Renderer>().material = RoomHeatmapMaterial;
	}
}
