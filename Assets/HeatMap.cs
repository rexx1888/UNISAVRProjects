using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMap : MonoBehaviour {

	private List<Analytic> roomAnalytics;
	[SerializeField] private Material RoomHeatmapMaterial;
	//[SerializeField] private Material ColdRoomMaterial;

	//[SerializeField] private Material[] materials;

	[SerializeField] Vector2 mapPointProperty;
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
		//if (roomAnalytics.Count > 0)
		//{
		//	//for (int i = 0; i < roomAnalytics.Count; i++)
		//	//{
		//	//	properties[i] = mapPointProperty;
		//	//}

		//	RoomHeatmapMaterial.SetInt("_Points_Length", totalCalculatedPoints);
		//	RoomHeatmapMaterial.SetVectorArray("_Points", positions);
		//	RoomHeatmapMaterial.SetVectorArray("_Properties", properties);
		//	Debug.Log(properties.Length);

		//}
	}

	public void setData(List<Analytic> givenAnalytics)
	{
		//roomAnalytics = givenAnalytics;
		////List<Analytic> CalculatedPoints = new List<Analytic>();

		//properties = new Vector4[roomAnalytics.Count];
		//Debug.Log(properties.Length);
		//positions = new Vector4[roomAnalytics.Count];

		//Mesh localMesh = GetComponent<Mesh>();
		//Vector3[] LocalVerticies = localMesh.vertices;
		//List<Color> meshColours = new List<Color>();

		////iterate through room specific data array
		//for (int i = 0; i < LocalVerticies.Length; i++)
		//{
		//	float h = 0;

		//	for (int x = 0; x < roomAnalytics.Count; x++)
		//	{
		//		float di = Vector3.Distance(LocalVerticies[i], roomAnalytics[x].Point);

		//		float ri = mapPointProperty.x;
		//		float hi = 1 - Mathf.Clamp(di / ri, 0, 1);

		//		h += hi * mapPointProperty.y;
		//	}

		//	h = Mathf.Clamp(h, 0, 1);

		//	meshColours.Add()
		//}


		//localMesh.SetColors()
		////Material[] mats = GetComponent<Renderer>().materials;

		//GetComponent<Renderer>().material = RoomHeatmapMaterial;
	}
}
