using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.Video;
using Pixelplacement;

public class ProgressTutorial : MonoBehaviour, IInteractable {

    public TuteController tuteController;
    [HideInInspector]
    public VRInteractiveItem vrII; //the VRInteractiveItem that is requried for interacting


    void Start()
    {
        //get the vr interactive item componenet
        vrII = this.GetComponent<VRInteractiveItem>();
        //safety check
        if (vrII != null)
        {
            //add the interact function to the double click delegate
            vrII.OnClick += Interact;
        }
    }

    public void Interact()
    {
        tuteController.ProgressInstructions();
    }

    public void OnHoverEnter()
    {
    }

    public void OnHoverExit()
    {
    }

}
