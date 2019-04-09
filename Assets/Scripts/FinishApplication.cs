using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.UI;

/// <summary>
/// 
/// Written by William Holman in October-November 2018.
/// 
/// This an object which uses the VRInteractiveItem, for documentation on what I've come to learn about how it works, check the programmer readme in the github repo.
/// 
/// this is used for displaying the 'analytics' objects. Each of these objects check how long they have been looked at, and the amount they were looked at. it was an experiment, i'm personally not happy with it.
/// 
/// Modified by Camryn Schriever 2019
/// 
/// 
/// </summary>

[RequireComponent(typeof(VRInteractiveItem))]
[RequireComponent(typeof(ScriptableObjectFloat))]
public class FinishApplication : MonoBehaviour, IInteractable {

    public ScriptableObjectFloat SceneTimer;

    public WallOfCubesController wallController; //the controller for the walls surrounding the camera object. 
    public VRInteractiveItem vrII; //the VRInteractiveItem attached to this.

	public Analytics analytics;

    //on start
    public void Start()
    {
        //get the vrInteractiveItem component attached to this
        vrII = this.GetComponent<VRInteractiveItem>();
        //safety check
        if (vrII != null)
        {
            //assign the functions to the delegates
            vrII.OnOver += OnHoverEnter;
            vrII.OnOut += OnHoverExit;
            vrII.OnDoubleClick += Interact;
        }

        //reset the timer to 0 when the scene begins.
        SceneTimer.value = 0;
    }

    //when this object is double clicked on
    public void Interact()
    {
        //if the state is in game
        if (GlobalStateManager.curState == GameState.InGame)
        {
            //enable the wall controller
            wallController.EnableObjects(true);
        } else if (GlobalStateManager.curState == GameState.Finished)
        {
            //otherwise disable it
            wallController.EnableObjects(false);


			DisplayUserViewLine();
        }
    }

    
    //when this object is looked at
    public void OnHoverEnter()
    {

    }

    //when this object is no longer looked at
    public void OnHoverExit()
    {

    }

    
    void Update()
    {
        //update the timer for every second.
        SceneTimer.value += Time.deltaTime;
    }

	void DisplayUserViewLine()
	{
		if (analytics.analyticsStorage.Count > 0)
		{
			//Analytic prevAnalytic = analytics.analyticsStorage[0];

			//Mesh connectionMesh = null;
			//GameObject connectionGO;
			//Vector3[] connectionVertices;
			//Color[] connectionColors;
			//int[] connectionTriangles;

			//Color colorConnectionNormal;
			//Color colorConnectionIntersecting;
			//float connectionWidthNormal = 1;
			//float connectionWidthIntersecting = 1;

			//if (connectionMesh == null)
			//{
			//	connectionMesh = new Mesh();
			//	connectionMesh.MarkDynamic();

			//	connectionGO = new GameObject("Connections");
			//	MeshRenderer mr = connectionGO.AddComponent<MeshRenderer>();
			//	mr.material = lineMaterial;
			//	mr.material.renderQueue = 3060;
			//	MeshFilter mf = connectionGO.AddComponent<MeshFilter>();
			//	mf.mesh = connectionMesh;
			//}

			//int connectionCount = seedConnections.Count;
			//int vertexCount = connectionCount * 4;
			//int triangleCount = connectionCount * 2;

			//connectionVertices = new Vector3[vertexCount];
			//connectionTriangles = new int[triangleCount * 3];
			//connectionColors = new Color[vertexCount];
			//Vector3[] normals = new Vector3[vertexCount];
			//Vector2[] uvs = new Vector2[vertexCount];

			//// setup normals
			//for (int i = 0; i < vertexCount; i++)
			//{
			//	normals[i] = Vector3.up;
			//	connectionColors[i] = Color.white;
			//}

			//// setup uv's
			//for (int i = 0; i < connectionCount; i++)
			//{
			//	uvs[i * 4 + 0] = Vector2.zero;
			//	uvs[i * 4 + 1] = Vector2.right;
			//	uvs[i * 4 + 2] = Vector2.zero;
			//	uvs[i * 4 + 3] = Vector2.right;
			//}

			//// setup triangles
			//for (int i = 0; i < connectionCount; i++)
			//{
			//	int index = i * 4;
			//	connectionTriangles[i * 6 + 0] = index + 0;
			//	connectionTriangles[i * 6 + 1] = index + 1;
			//	connectionTriangles[i * 6 + 2] = index + 2;
			//	connectionTriangles[i * 6 + 3] = index + 1;
			//	connectionTriangles[i * 6 + 4] = index + 3;
			//	connectionTriangles[i * 6 + 5] = index + 2;
			//}

			//connectionMesh.Clear();
			//connectionMesh.vertices = connectionVertices;
			//connectionMesh.triangles = connectionTriangles;
			//connectionMesh.colors = connectionColors;
			//connectionMesh.normals = normals;
			//connectionMesh.uv = uvs;
			//connectionMesh.bounds = new Bounds(Vector3.zero, Vector3.one * 10);

			//UpdateConnectionMesh();

			foreach (Analytic analytic in analytics.analyticsStorage)
			{

				

				//prevAnalytic = analytic;
			}
		}		
	}
}
