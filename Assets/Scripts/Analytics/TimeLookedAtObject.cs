using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.UI;

/// <summary>
/// 
/// Written by William Holman in October-November 2018.
/// 
/// This an object which uses the VRInteractiveItem class, for documentation on what I've come to learn about how it works, check the programmer readme in the github repo.
/// 
/// </summary>

[RequireComponent(typeof(VRInteractiveItem))]
public class TimeLookedAtObject : MonoBehaviour, IInteractable {


    private VRInteractiveItem vrInteractiveItem; //the vrInteractiveItem
    public float totalTimeLookedAt; //total time this object has been looked at
    public int amountLookedAt; //amount of times its been looked at
    public bool curLookedAt = false; //is it currently being looked at

    public Text statTextBox; //text box to fill out the stats

    // Use this for initialization
    void Start () {

        //get the VRInteractiveItem component attached to this
        vrInteractiveItem = this.GetComponent<VRInteractiveItem>();
        //safety check
        if (vrInteractiveItem != null)
        {
            //add the functions to the delegate
            vrInteractiveItem.OnOver += OnHoverEnter;
            vrInteractiveItem.OnOut += OnHoverExit;
        }

	}

    //called when this object is double clicked on
    public void Interact()
    {
        //N/A
    }

    //when this object is looked at
    public void OnHoverEnter()
    {
        //set this to true
        curLookedAt = true;
        //if we are in game
        if (GlobalStateManager.curState == GameState.InGame)
        {
            //increase the amount looked at
            amountLookedAt++;
        }
    }

    //when this is no longer being looked at
    public void OnHoverExit()
    {
        //set this to false
        curLookedAt = false;
    }


    // Update is called once per frame
    void Update() {

        //if we are in game
        if (GlobalStateManager.curState == GameState.InGame)
        {
            //and currently being looked at
            if (curLookedAt)
            {
                //increase the time by delta time
                totalTimeLookedAt += Time.deltaTime;
            }
        }

        //if currently looked at
        if (curLookedAt)
        {
            //and the gamestate is finished
            if (GlobalStateManager.curState == GameState.Finished)
            {
                //and the text box is not null
                if (statTextBox != null)
                {
                    //update the text box
                    UpdateTextBox();
                }
            }
        }
        else
        {
            //otherwise make sure there's no text
            statTextBox.text = "";
        }

    }

    //update the text box with the stats from this object
    public void UpdateTextBox()
    {

        //fill out the text box with the values
        statTextBox.text = "Total time looked at: " + totalTimeLookedAt + "\n Total amount looked at: " + amountLookedAt;
    }

}

