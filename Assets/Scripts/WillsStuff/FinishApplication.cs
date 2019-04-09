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


}
