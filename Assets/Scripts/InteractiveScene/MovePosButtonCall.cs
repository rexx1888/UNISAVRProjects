using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.Video;


[RequireComponent(typeof(VRInteractiveItem))]
public class MovePosButtonCall : MonoBehaviour, IInteractable
{

    public VRInteractiveItem VRInteractiveScript; //the VRInteractiveItem that is requried for interacting
    public MovePosition movScript;
    public GameObject goalPos;


    // Use this for initialization
    void Start() {
        VRInteractiveScript = this.GetComponent<VRInteractiveItem>();
    }

    // Update is called once per frame
    void Update() {

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

    //when this object is double clicked on
    public void Interact()
    {
        //if we are in game
        if (GlobalStateManager.curState == GameState.InGame)
        {
            movScript.ButtonMove(goalPos.transform.position);
        }
    }
}



