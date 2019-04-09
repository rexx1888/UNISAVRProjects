using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

/// <summary>
/// 
/// Written by William Holman in October-November 2018.
/// 
/// This an object which uses the VRInteractiveItem, for documentation on what I've come to learn about how it works, check the programmer readme in the github repo.
/// 
/// This is used for inspecting an object. The object exists in the world, and when the player looks at it, if the difficulty is assisted, it will have an indicator pop up around the object. un-assisted does not have this. 
/// when player double clicks when looking at this object, it will then trigger the CanvasController on the camera and spawn the inspection canvas in front of the camera, and the inspetableObject to inspect.
/// 
/// </summary>


[RequireComponent(typeof(VRInteractiveItem))]
public class InspectableObject : MonoBehaviour, IInteractable
{
    [Tooltip("the item that you want to inspect")]
    public GameObject itemToSpawnWhenInteractedWith; //item to inspect (should have the spin class on it to be able to spin around when player swipes on touchpad)
    [Tooltip("The highlight gameobject that will appear when the view is on this object")]
    public GameObject objectHighlight; //the indicator that turns on when this object is looked at on assisted difficulty

    [Tooltip("CanvasObject to create, this is the inspect canvas")]
    public GameObject canvasToFadeIn; //this is the canvas to spawn when inspecting, the "inspect canvas" 

    [Header("The name of the object and whether it should be counted or not, ie: don't count tutorial items.")]
    public bool itemIsTracked = true; //is this item tracked on the global state manager's list of tracked items?
    [Tooltip("The name of the object to be inspected, eg: Rusty Knife")]
    public string nameOfInspectableObject = "Blank"; //the name of this object, used if tracked

    private VRInteractiveItem interactiveItem; //the vr interactive item attached to this

	// Use this for initialization
	void Start () {
        //try to get the VRInteractiveItem component attached to this.
        interactiveItem = this.gameObject.GetComponent<VRInteractiveItem>();
        //if it exists
        if (interactiveItem != null)
        {
            //add the functions to the delegates
            interactiveItem.OnDoubleClick += Interact;
            interactiveItem.OnOver += OnHoverEnter;
            interactiveItem.OnOut += OnHoverExit;
        }
    }

    //when this object is double tapped on
    public void Interact()
    {
        //if the current state is in game.
        if (GlobalStateManager.curState == GameState.InGame)
        {
            //and the item to spawn when interacted with is not null
            if (itemToSpawnWhenInteractedWith != null)
            {
                //if the item is tracked
                if (itemIsTracked)
                {
                    //call the global states manager to add an inspection.
                    GlobalStateManager.AddInspection(nameOfInspectableObject);
                }
                //if the object highlight exists, make sure its not active whilst inspecting
                if (objectHighlight != null)
                {
                    objectHighlight.SetActive(false);
                }
                //get the canvas controller component from the camera.
                CanvasController cc = Camera.main.GetComponent<CanvasController>();
                //if it exists
                if (cc != null)
                {
                    //change the state and canvas, to spawn in the inspect canvas and the inspectable object
                    cc.ChangeCanvasAndState(null, canvasToFadeIn, GameState.Inspecting, itemToSpawnWhenInteractedWith);
                }
                
            }
        }
    }

    //called when this object is looked at
    public void OnHoverEnter()
    {
        //if the difficulty is assisted and we are in game
        if (GlobalStateManager.curDifficulty == Difficulty.Assisted && GlobalStateManager.curState == GameState.InGame)
        {
            //turn on the indicator
            if (objectHighlight != null)
                objectHighlight.SetActive(true);
        }
    }

    //called when this object is no longer looked at
    public void OnHoverExit()
    {
        //if the difficulty is assisted and we are in game
        if (GlobalStateManager.curDifficulty == Difficulty.Assisted && GlobalStateManager.curState == GameState.InGame)
        {
            //disable the indicator
            if (objectHighlight != null)
                objectHighlight.SetActive(false);
        }
    }

}
