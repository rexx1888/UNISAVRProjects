using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

/// <summary>
/// This script is used in the interactive VR scenes to allow the camera to move.
/// Written by David James McCann, Designer, 09Apr2019
/// </summary>


public class MovePosition : MonoBehaviour {

    private VRInput input;

    bool targetGood = false;


    // Use this for initialization
    void Start () {
        input = Camera.main.GetComponent<VRInput>();


        input.OnClick += ClickMove; //i guess im assigning to a delegate now. Thats a thing...
       
	}
	

    public void ClickMove()
    {


        Vector3 targetPoint = CheckRayCastHit();
        if(targetGood != false)
        {
            gameObject.transform.position = targetPoint;

            targetGood = false;
        }
    }

    Vector3 CheckRayCastHit()
    {
        Vector3 point = Vector3.zero;

        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit ))
        {
			Debug.Log(hit.transform.name);
            if(hit.normal == Vector3.up)
            {
                point = hit.point;
                targetGood = true;
            }
        }

        return point;
    }

}
