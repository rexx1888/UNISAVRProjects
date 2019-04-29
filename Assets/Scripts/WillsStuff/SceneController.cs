using System.Collections;
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

    public List<DisplayObject> baseObjectList; //the list of skybox dependent objects
    public DisplayObject userInterface;
    public GameObject player;
    public CurrentSceneState currentSceneState;
    [HideInInspector]
    public VideoPlayer videoPlayer; //the object playing a video
    public bool testui = false;
    public float userInterfaceDistance;
    [SerializeField]protected VRInput c_Input;

    protected SceneStateChangeStuff sceneStateCheck;

    public void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        c_Input.OnHold += ShowUI;
             
        foreach(SceneStateChangeStuff sSCS in currentSceneState.sceneStuff)
        {
            sSCS.spawnedAnalyticTracker = Instantiate(sSCS.analyticTrackerObject, player.transform.position, player.transform.rotation).GetComponent<DisplayObject>();
            GetComponent<RenderViewData>().raycastTrackerObjects.Add(sSCS.spawnedAnalyticTracker.gameObject.transform);
        }
        SwitchRoom();
        foreach(ChangeSkyBox cSB in gameObject.GetComponentsInChildren<ChangeSkyBox>())
        {
            cSB.vrII = GetComponent<VRInteractiveItem>();
            cSB.controller = gameObject.GetComponent<SceneController>();

        }
        userInterface.SetActive(true);
        GetComponentInChildren<FinishApplication>().rvd = GetComponent<RenderViewData>();
        GetComponentInChildren<CloseUI>().userInterface = userInterface;
        userInterface.SetActive(false);
    }

    private void Update()
    {

        if(currentSceneState.currentScene != sceneStateCheck) //if the scene is supposed to change
        {
            SwitchRoom();
        }

    }

    //enables the object passed in if its in the list, and disables all others in the list that are not that one
    public void EnableObject(DisplayObject objToEnable)
    {

        //foreach gameobject in the list
        foreach (DisplayObject go in baseObjectList)
        {
            //if the passed in one is the same as the current iteration
            if (go == objToEnable)
            {
                //enabled the object
                go.SetActive(true);
            } else
            {
                //disable the object if it is not the same as the one passed in
                go.SetActive(false);
            }
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
    }
}
