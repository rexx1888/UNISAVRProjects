using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.UI;

/// <summary>
/// 
/// 
/// Written by William Holman in October-November 2018.
/// 
/// This an object which uses the VRInteractiveItem class, for documentation on what I've come to learn about how it works, check the programmer readme in the github repo.
/// 
/// this class checks if this object is interacted with,and therefore turns a toggle on if it is.
/// 
/// </summary>

[RequireComponent(typeof(VRInteractiveItem))]
public class TutorialInspectToggleListener : MonoBehaviour {

    public Toggle toggleToAffect; //the toggle to check for

    //on start
    public void Start()
    {
        //get the vrInteractiveItem component attached to this
        VRInteractiveItem vrInteractiveItem = this.GetComponent<VRInteractiveItem>();
        //safety check
        if (vrInteractiveItem != null)
        {
            //add the function to the delegate
            vrInteractiveItem.OnDoubleClick += TurnOnToggle;
        }
    }

    //turns on the toggle attached
    public void TurnOnToggle()
    {
        //turn on the toggle
        toggleToAffect.isOn = true;
        //get the vrInteractiveItem component attached to this
        VRInteractiveItem vrInteractiveItem = this.GetComponent<VRInteractiveItem>();
        //safety check
        if (vrInteractiveItem != null)
        {
            //remove the function from the delegate
            vrInteractiveItem.OnDoubleClick -= TurnOnToggle;
        }
    }
}
