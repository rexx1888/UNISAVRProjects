using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTracker : MonoBehaviour {

    public Camera mainCamera;
    public ParticleSystem particleSystem;
    private ParticleSystem.EmitParams emitParams;

    public float timerInterval = 0.5f;
	public ScriptableObjectFloat sceneTimer;

	private float localTimer;
	private int layermask = LayerMask.GetMask("ViewTracker");

	public Analytics analytics;


	// Use this for initialization
	void Start () {
		//emitParams = new ParticleSystem.EmitParams;
		localTimer = 0;
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
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
	
    private void storePoint()
    {
		//raycast from the camera looking fowards
        RaycastHit hit;
        Ray ray = new Ray(transform.position, mainCamera.transform.forward);

		//Reverse the ray, so it can collide with the Sphere for tracking where the user is looking.
        Ray testRay = new Ray(ray.GetPoint(100), -mainCamera.transform.forward);
        Debug.DrawRay(ray.GetPoint(100), -mainCamera.transform.forward);
		

        if (Physics.Raycast(testRay, out hit, 150, layermask))
        {
			//Debug.Log(hit.transform.name);
			Debug.Log(hit.point);

			//store the location of where the user was looking at the scene.
			analytics.addAnalytic(sceneTimer.value, "Test", hit.point);		
        }
    }
}
