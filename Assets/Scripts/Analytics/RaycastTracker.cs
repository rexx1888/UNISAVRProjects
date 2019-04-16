using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTracker : MonoBehaviour {

    public Camera mainCamera;
	//public ParticleSystem particleSystem;
	//private ParticleSystem.EmitParams emitParams;

	[SerializeField] private float timerInterval = 0.5f;
	[SerializeField] private ScriptableObjectFloat sceneTimer;
	[SerializeField] private Analytics analytics;

	private float localTimer;
	private int layermask;


	// Use this for initialization
	void Start () {
		//emitParams = new ParticleSystem.EmitParams;
		localTimer = 0;
		layermask = LayerMask.GetMask("ViewTracker");
	}
	
	// Update is called once per frame
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
	
    private void storePoint()
    {
		//raycast from the camera looking fowards
        RaycastHit hit;
        Ray ray = new Ray(transform.position, mainCamera.transform.forward);

		//Reverse the ray, so it can collide with the Sphere for tracking where the user is looking.
        Ray testRay = new Ray(ray.GetPoint(100), -mainCamera.transform.forward);
        //Debug.DrawRay(ray.GetPoint(100), -mainCamera.transform.forward);
		

        if (Physics.Raycast(testRay, out hit, 150, layermask))
        {			
			//store the location and Name of where the user was looking at the scene.
			analytics.addAnalytic(sceneTimer.value, hit.transform.name, hit.point);
			//Debug.Log(hit.transform.name);
        }
    }
}
