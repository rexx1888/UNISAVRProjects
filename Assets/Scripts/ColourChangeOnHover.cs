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
/// When this object is looked at, it will change the colour of the imageToChangeColourOf image to the hoverColour.
/// when this object is no longer being looked at, it will change back.
/// 
/// </summary>

[RequireComponent(typeof(VRInteractiveItem))]
public class ColourChangeOnHover : MonoBehaviour, IInteractable {

    VRInteractiveItem vrII; //the VRInteractiveItem attached
    public Image imageToChangeColourOf; //the image who's colour should be affected.

    public Color normalColour; //the colour when not being looked at
    public Color hoverColour; //the colour when being looked at

    // Use this for initialization
    void Start () {
        //get the VRInteractiveItem component
        vrII = this.GetComponent<VRInteractiveItem>();
        //safety check
        if (vrII != null)
        {
            //add the functions to the delegates.
            vrII.OnOver += OnHoverEnter;
            vrII.OnOut += OnHoverExit;
        }
    }

    //when this object is double clicked on
    public void Interact()
    {
        //N/A
    }

    //when this object is looked at
    public void OnHoverEnter()
    {
        //if the image exists
        if (imageToChangeColourOf != null)
        {
            //change the colour
            imageToChangeColourOf.color = hoverColour;
        }
    }

    //when this object is no longer looked at
    public void OnHoverExit()
    {
        //if the image exists
        if (imageToChangeColourOf != null)
        {
            //change the colour
            imageToChangeColourOf.color = normalColour;
        }
    }
}
