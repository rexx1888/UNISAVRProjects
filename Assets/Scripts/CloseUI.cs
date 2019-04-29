using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;

public class CloseUI : MonoBehaviour, IInteractable
{
    [HideInInspector]
    public DisplayObject userInterface;
    //on awake
    public void Awake()
    {
        //get the vr Interactive Item component attached to this object. 
        VRStandardAssets.Utils.VRInteractiveItem vrII = this.GetComponent<VRStandardAssets.Utils.VRInteractiveItem>();
        //if it exists
        if (vrII != null)
        {
            //add the functions to the delegate
            vrII.OnClick += Interact;
        }
    }

    //when this object is double clicked on
    public void Interact()
    {
        userInterface.SetActive(false);
    }

    //when this object is looked at
    public void OnHoverEnter()
    {
        //N/A
    }

    //when this object is no longer looked at
    public void OnHoverExit()
    {
        //N/A
    }
}
