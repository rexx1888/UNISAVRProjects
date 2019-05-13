using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(MeshFilter))]
public class InvertMesh : MonoBehaviour {

    //void Start()
    //{
    //    MeshFilter mf = GetComponent<MeshFilter>();
    //    Mesh mesh = mf.mesh;
    //    InsideOut(ref mesh);
    //    AssetDatabase.CreateAsset(mesh, "Assets/Art/InvertedMeshes/InvertedMesh" + gameObject.name + ".asset");
    //    AssetDatabase.SaveAssets();
    //}

    //void InsideOut(ref Mesh mesh)
    //{
    //    int[] triangles = mesh.triangles;
    //    for (int i = 0; i < triangles.Length; i += 3)
    //    {
    //        int tmp = triangles[i + 1];
    //        triangles[i + 1] = triangles[i + 2];
    //        triangles[i + 2] = tmp;
    //    }
    //    mesh.triangles = triangles;
    //    mesh.RecalculateNormals();
    //    mesh.RecalculateTangents();
    //}
}

