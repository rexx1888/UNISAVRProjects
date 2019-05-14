﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTracker : MonoBehaviour {

    public Camera mainCamera;

	[Header("Timer interval between analytics")]
	[SerializeField] private float timerInterval = 0.5f;

	[Header("Scriptable Objects")]
	[SerializeField] private ScriptableObjectFloat sceneTimer;
	[SerializeField] private Analytics analytics;

	//time between each analytic being recorded.
	private float localTimer;
	private int layermask;


	// Use this for initialization
	void Start () {
		localTimer = 0;

		//reset the timer to 0 when the scene begins.
		sceneTimer.value = 0;

		layermask = LayerMask.GetMask("ViewTracker");
	}
	
	void FixedUpdate ()
    {
		if (GlobalStateManager.curState == GameState.InGame)
		{
			//increment local timer by delta time
			localTimer += Time.deltaTime;

			//if localTimer is larger than the interval, reset localtimer and do the thing.
			if (localTimer > timerInterval)
			{
				storePoint();

				localTimer = 0;
			}
		}
	}

	private void Update()
	{
		if (GlobalStateManager.curState == GameState.InGame)
		{
			//update the timer for every second.
			sceneTimer.value += Time.deltaTime;
		}
	}

	private void storePoint()
    {
		//raycast from the camera looking fowards
        RaycastHit hit;
        Ray ray = new Ray(transform.position, mainCamera.transform.forward);

		//Reverse the ray, so it can collide with the Sphere for tracking where the user is looking.
        Ray testRay = new Ray(ray.GetPoint(100), -mainCamera.transform.forward);		

        if (Physics.Raycast(testRay, out hit, 150, layermask))
        {			
			//store the location and Name of where the user was looking at the scene.
			analytics.addAnalytic(sceneTimer.value, hit.transform.name, hit.point);
        }
    }
}
