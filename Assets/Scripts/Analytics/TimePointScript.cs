﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class TimePointScript : MonoBehaviour {

	//the timeCode is a child (Since textmesh and Mesh Filter don't like eachother)
	[SerializeField] private TextMeshPro textMesh;
	[SerializeField] private Transform textMeshTransform;

	public void OnCreatePoint(Vector3 position, Transform room, float timeCode)
	{

		//format from seconds to minutes and seconds.
		int minutes = Mathf.FloorToInt(timeCode / 60F);
		int seconds = Mathf.FloorToInt(timeCode - minutes * 60);
		string formattedTime = string.Format("{0:0}:{1:00}", minutes, seconds);

		//set text mesh info
		textMesh.text = formattedTime;

		//set parent (for activating and deactivating between rooms) and position.
		transform.SetParent(room);
		transform.position = position;

		//make the text look at the camera
		textMeshTransform.LookAt(Camera.main.transform.position);
		textMeshTransform.rotation = Quaternion.LookRotation(-textMeshTransform.forward);
	}	
}
