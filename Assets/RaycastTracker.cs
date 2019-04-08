using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTracker : MonoBehaviour {

    public Camera mainCamera;
    public ParticleSystem ParticleSystem;

    private ParticleSystem.EmitParams emitParams;

	// Use this for initialization
	void Start () {
        //emitParams = new ParticleSystem.EmitParams;

    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        RaycastHit hit;
        //Debug.Log("UPDATE");
        Ray ray = new Ray(transform.position, transform.forward);

        ray.GetPoint(105);
        ray.direction = -ray.direction;
        Debug.DrawRay(ray.GetPoint(105), ray.direction);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.transform.name);

            if (hit.transform.tag == "HeatMapCapsule")
            {
                //var emitParams = new ParticleSystem.EmitParams();
                emitParams.position = hit.point;
                emitParams.velocity = Vector3.zero;
                ParticleSystem.Emit(emitParams, 1);
                
            }
        }
        
	}
}
