using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// Written by William Holman in October-November 2018.
/// 
/// This an object which uses the VRInteractiveItem, for documentation on what I've come to learn about how it works, check the programmer readme in the github repo.
/// 
/// 
/// </summary>


public class ReloadScene : MonoBehaviour, IInteractable {

    public int sceneToLoad;

    //on awake
    public void Awake()
    {
        //get the vr Interactive Item component attached to this object. 
        VRStandardAssets.Utils.VRInteractiveItem vrII = this.GetComponent<VRStandardAssets.Utils.VRInteractiveItem>();
        //if it exists
        if (vrII != null)
        {
            //add the functions to the delegate
            vrII.OnDoubleClick += Interact;
        }
    }

    //when this object is double clicked on
    public void Interact ()
    {
        //load the scene at int
        SceneManager.LoadScene(sceneToLoad);
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
