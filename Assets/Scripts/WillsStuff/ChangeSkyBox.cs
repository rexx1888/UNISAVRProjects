using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.Video;

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
/// </summary>
[RequireComponent(typeof(VRInteractiveItem))]
public class ChangeSkyBox : MonoBehaviour, IInteractable {

    public VRInteractiveItem vrII; //the VRInteractiveItem that is requried for interacting
    public SkyboxObjectEnableController skyboxChanger; //the object that controls the list of each skybox dependent object.
    public GameObject objToEnable; //the skybox dependent object to enable for this skybox material
    public VideoClip newSkyboxclip; //the material to change the skybox to
    public GameObject videoPlayer; //the object playing a video

    //this was used to intend to fade the screen to black before changing the skybox, to avoid sudden movement. 
    public float fadeDuration = 1.0f; //how long to fade the screen in/out

        //on start
    public void Start()
    {
        //get the vr interactive item componenet
        vrII = this.GetComponent<VRInteractiveItem>();
        //safety check
        if (vrII != null)
        {
            //add the interact function to the double click delegate
            vrII.OnDoubleClick += Interact;
        }
    }

    //when this object is double clicked on
    public void Interact()
    {
        //if we are in game
        if (GlobalStateManager.curState == GameState.InGame)
        {
            //if everything isn't null
            if (skyboxChanger != null && objToEnable != null && newSkyboxclip != null)
            {
                //enable the skybox dependent object, disable the others
                skyboxChanger.EnableObject(objToEnable);
                //change the skybox material
                //RenderSettings.skybox = newSkyboxMaterial;
                videoPlayer.GetComponent<VideoPlayer>().clip = newSkyboxclip;

                //was intended to fade the screen in then out, however did not work as intended. 
                //StartCoroutine(FadeInThenOut());
            }
        }
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

    //coroutine for fading the screen in, changing the skybox, then fading out
    public IEnumerator FadeInThenOut()
    {
        //change the global state to ensure that nothing can be interacted with
        GlobalStateManager.ChangeState(GameState.Loading);
        //try to get the camera fade component from the camera
        VRCameraFade vrCamFade = Camera.main.GetComponent<VRCameraFade>();
        //if it exists
        if (vrCamFade != null)
        {
            //wait until the screen is faded in
            yield return StartCoroutine(vrCamFade.BeginFadeIn(fadeDuration, false));
        }

        //enable the skybox dependent object, disable the others
        skyboxChanger.EnableObject(objToEnable);
        //change the skybox material
        //RenderSettings.skybox = newSkyboxclip;
        videoPlayer.GetComponent<VideoPlayer>().clip = newSkyboxclip;

        //if the camera fade component exists
        if (vrCamFade != null)
        {
            //wait until screen is faded out
            yield return StartCoroutine(vrCamFade.BeginFadeOut(fadeDuration, false));
        }

        //resume play.
        GlobalStateManager.ChangeState(GameState.InGame);
    }

}
