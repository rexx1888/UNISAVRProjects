using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Written by William Holman in October-November 2018.
/// 
/// This is for changing the global state when a scene is loaded.
/// 
/// </summary>

public class ChangeGameStateOnEnable : MonoBehaviour {

    
    public GameState stateToChangeTo; //state to change the global state to

    //if this object is enabled
    public void OnEnable()
    {
        //change the global state
        GlobalStateManager.ChangeState(stateToChangeTo);
    }

    //on awake
    public void Awake()
    {
        //change the global state
        GlobalStateManager.ChangeState(stateToChangeTo);
    }

    //on start
    public void Start()
    {
        //change the global state
        GlobalStateManager.ChangeState(stateToChangeTo);
    }
}
