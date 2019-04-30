using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.Video;
using Pixelplacement;

/// <summary>
/// 
/// Written by William Holman in October-November 2018.
/// 
/// This an object which uses the VRInteractiveItem, for documentation on what I've come to learn about how it works, check the programmer readme in the github repo.
/// 
/// This object, when interacted with by double pressing the touchpad whilst looking at it
/// will attempt to change the skybox material to a new one. it will also try to enable a certain object and disable other objects that are referenced in the SkyboxObjectEnableController class.
/// this is due to each skybox (360 photo) needing different sets of InteractiveItems, so disable the ones that are for different skyboxes and enabled the one intended for this skybox. 
/// 
/// Modified by Camryn Schriever 2019
/// 
/// </summary>

[RequireComponent(typeof(VRInteractiveItem))]
public class ChangeSkyBox : MonoBehaviour, IInteractable {

    [HideInInspector]
    public VRInteractiveItem vrII; //the VRInteractiveItem that is requried for interacting
    [HideInInspector]
    public SceneController controller; //the object that controls the list of each skybox dependent object.
    public SceneStateChangeStuff sceneSpecificStuff;

    public void Start()
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

    //when this object is double clicked on
    public void Interact()
    {
        //controller.EnableObject(objToEnable.GetComponent<DisplayObject>());
        controller.currentSceneState.currentScene = sceneSpecificStuff;
		Debug.Log(sceneSpecificStuff.name);
    }

    //when this object is first looked at
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
