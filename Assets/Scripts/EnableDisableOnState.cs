using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Written by William Holman in October-November 2018.
/// 
/// enable/disable an object if the global state manager is equal to the required state
/// 
/// </summary>

public class EnableDisableOnState : MonoBehaviour {

    public GameState stateToEnableOn = GameState.InGame; //state required for enabling
    public GameObject objToEnable; //object to enable/disable


	// Update is called once per frame
	void Update () {
		
        //if there is an object
        if (objToEnable != null)
        {
            //if the state required is the current state
            if (GlobalStateManager.curState == stateToEnableOn)
            {
                //if the object is not already active
                if (!objToEnable.activeSelf)
                {
                    //activate it
                    objToEnable.SetActive(true);
                }
            } else //otherwise
            {
                //if it is active
                if (objToEnable.activeSelf)
                {
                    //deactivate it
                    objToEnable.SetActive(false);
                }
            }
        }
	}
}
