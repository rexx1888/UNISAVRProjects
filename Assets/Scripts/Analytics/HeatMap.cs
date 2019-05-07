using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMap : MonoBehaviour {

	private List<Analytic> roomAnalytics;
	[SerializeField] private Material RoomHeatmapMaterial;
	[SerializeField] Vector2 mapPointProperty;
	[SerializeField] public Vector4[] properties;
	[SerializeField] public Vector4[] positions;
	[SerializeField] public Texture2D heatMapGradient;

	// Use this for initialization
	void Start ()
	{
		roomAnalytics = new List<Analytic>();
	}
	

	public void setData(List<Analytic> givenAnalytics)
	{
		roomAnalytics = givenAnalytics;
		properties = new Vector4[roomAnalytics.Count];
		positions = new Vector4[roomAnalytics.Count];
		MeshFilter localMesh = GetComponent<MeshFilter>();

		//get the verticies and convert to worldspace.
		Vector3[] LocalVerticies = localMesh.mesh.vertices;
		LocalVerticies = LocalToWorldSpaceVert(LocalVerticies, transform);

		Color[] meshColours = new Color[LocalVerticies.Length];


		//iterate through room specific data array
		for (int i = 0; i < LocalVerticies.Length; i++)
		{
			float h = 0;

			for (int x = 0; x < roomAnalytics.Count; x++)
			{
				float di = Vector3.Distance(LocalVerticies[i], roomAnalytics[x].Point);

				float ri = mapPointProperty.x;
				float hi = 1 - Mathf.Clamp(di / ri, 0, 1);

				h += hi * mapPointProperty.y;
			}

			h = Mathf.Clamp(h, 0, 1);

			meshColours[i] = heatMapGradient.GetPixelBilinear(h, 0.5f);
		}



		localMesh.mesh.colors = meshColours;
		GetComponent<Renderer>().material = RoomHeatmapMaterial;
	}

	public Vector3[] LocalToWorldSpaceVert(Vector3[] LocalVerticies, Transform owner)
	{
		for (int i = 0; i < LocalVerticies.Length; i++)
		{
			LocalVerticies[i] = owner.localToWorldMatrix.MultiplyPoint3x4(LocalVerticies[i]);
		}

		return LocalVerticies;
	}
}
