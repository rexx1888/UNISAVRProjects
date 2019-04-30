﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using UnityEngine.Video;
using VRStandardAssets.Utils;

/// <summary>
/// 
/// Written by William Holman in October-November 2018.
/// 
/// this object is for controlling the base objects for each skybox dependent object, and enabling/disabling them when needed
/// 
/// </summary>

public class SceneController : MonoBehaviour {

    public DisplayObject userInterface;
    public GameObject player;
    public CurrentSceneState currentSceneState;
    [HideInInspector]
    public VideoPlayer videoPlayer; //the object playing a video
    public bool testui = false;
    public float userInterfaceDistance;
    [SerializeField]protected VRInput c_Input;

    protected List<ChangeSkyBox> buttons = new List<ChangeSkyBox>();

    protected SceneStateChangeStuff sceneStateCheck;

    public void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        c_Input.OnHold += ShowUI;
         
        foreach(SceneStateChangeStuff sSCS in currentSceneState.sceneStuff)
        {
            sSCS.spawnedAnalyticTracker = Instantiate(sSCS.analyticTrackerObject, player.transform.position, player.transform.rotation).GetComponent<DisplayObject>();
            GetComponent<RenderViewData>().raycastTrackerObjects.Add(sSCS.spawnedAnalyticTracker.gameObject);
        }
        userInterface.SetActive(true);
        foreach (ChangeSkyBox cSB in gameObject.GetComponentsInChildren<ChangeSkyBox>())
        {
            buttons.Add(cSB);
            cSB.vrII = GetComponent<VRInteractiveItem>();
            cSB.controller = gameObject.GetComponent<SceneController>();
            cSB.gameObject.AddComponent<DisplayObject>();

        }
        SwitchRoom();

        GetComponentInChildren<FinishApplication>().rvd = GetComponent<RenderViewData>();
        GetComponentInChildren<CloseUI>().userInterface = userInterface;
        userInterface.SetActive(false);
    }

    private void Update()
    {

        if(currentSceneState.currentScene != sceneStateCheck) //if the scene is supposed to change
        {
            Debug.Log("state change");
            SwitchRoom();
        }

    }

    //displays the UI in front the user when they 'hold' down the control(to set Hold time, check VRInput)
    public void ShowUI()
    {
        //direction * distance + sighting position
        userInterface.transform.position = Camera.main.transform.forward * userInterfaceDistance + player.transform.position; //not finished yet
        userInterface.SetActive(true);
    }

    public void SwitchRoom()
    {
        //change the skybox
        videoPlayer.clip = currentSceneState.currentScene.clip;

        //change the UI
        currentSceneState.currentScene.spawnedAnalyticTracker.SetActive(true);
        foreach (SceneStateChangeStuff sSCS in currentSceneState.sceneStuff)
        {
            if (currentSceneState.currentScene != sSCS)
            {
                sSCS.spawnedAnalyticTracker.SetActive(false);
            }
        }

        foreach(ChangeSkyBox cSB in buttons)
        {
            if(currentSceneState.currentScene != cSB.sceneSpecificStuff)
            {
                cSB.gameObject.GetComponent<DisplayObject>().SetActive(true);
            }else
            {
                cSB.gameObject.GetComponent<DisplayObject>().SetActive(false);

            }
        }

		sceneStateCheck = currentSceneState.currentScene;
	}
}
