using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// Written by William Holman, October-November 2018.
/// 
/// This uses the VRStandardAssets VRInteractiveItem class. I've explained what I've come to learn about the VRStandardAssets classes in the programmer readme in the git repo. 
/// 
/// This will get activated when the object this is attached to is double clicked on, using the touch pad. 
/// 
/// This class is used for a variety of reasons.
/// There are three uses for it.
/// 
/// 1. Changing canvases
///     if UsesCanvases is ticked, it will try to get the CanvasController component on the main camera.
///     it will then attempt to call the change canvas and state function, if the component exists.
///     this was for swapping between canvases in front of the camera, however this functionality in the simulation was removed because it was no longer wanted.
///     I decided to leave it in, just in case it's wanted again.
///     
/// 2. Setting difficulty.
///    if setDifficulty is ticked, it will attempt to set the difficulty on the global state manager.
///    There are two difficulties at the time of writing this, Assisted and Un-Assisted.
///    Assisted allows an icon to pop up that indicates what you are looking at.
///    Un-Assisted has no indicators when looking at inspectable objects. 
///    
/// 3. Loading Scenes
///    if loadScene is ticked, it will attempt to load the specified scene (through int value)
///    
/// 
/// </summary>


[RequireComponent(typeof(VRStandardAssets.Utils.VRInteractiveItem))]
public class ButtonToChangeState : MonoBehaviour, IInteractable {

    private VRStandardAssets.Utils.VRInteractiveItem vrII;

    #region Uses Canvas Region
    public bool UseCanvases = true; //does this want to swap canvas?
    public GameObject canvasToFadeInto; //what canvas do you want to swap to?
    public GameState stateToChangeTo; //what state do you want to swap to?
    bool alreadyUsed = false; //ensures that this only does it once at a time
    #endregion

    #region Set Difficulty Region
    public bool setDifficulty = false; //do you want to change difficulty?
    public Difficulty difficultyToChangeTo = Difficulty.UnAssisted; //what difficulty would you like to swap to?
    #endregion

    #region Load A Scene Region
    public bool loadScene = false; //would you like to load a scene?
    public int sceneToLoad; //what scene would you like to load?
    #endregion


    // Use this for initialization
    void Awake () {

        //Gets the required VRInteractiveItem componenent attached to this object.
        vrII = this.GetComponent<VRStandardAssets.Utils.VRInteractiveItem>();
        //forced to exist, but to be safe, make sure it exists.
        if (vrII != null)
        {
            //add this classes interactive functions to the VRInteractiveItem delegates.
            vrII.OnOver += OnHoverEnter;
            vrII.OnOut += OnHoverExit;
            vrII.OnDoubleClick += Interact;
        }
        //is false on startup, make sure it is
        alreadyUsed = false;
    }
    

    //called if double clicked on
    public void Interact()
    {

        //if uses canvases
        if (UseCanvases)
        {
            //if its not already being used
            if (alreadyUsed == false)
            {
                //get the canvas controller component from the camera
                CanvasController cc = Camera.main.GetComponent<CanvasController>();
                //if it exists
                if (cc != null)
                {
                    //this button is now already used
                    alreadyUsed = true;
                    //change the canvas and the state on the canvas controller
                    cc.ChangeCanvasAndState(this.transform.root.gameObject, canvasToFadeInto, stateToChangeTo, null);
                }
            }
        }
        
        //if this sets difficulty
        if (setDifficulty)
        {
            //change the difficulty in the global state manager
            GlobalStateManager.curDifficulty = difficultyToChangeTo;
        }

        //if this changes scene
        if (loadScene)
        {
            //load the new scene
            SceneManager.LoadScene(sceneToLoad);
        }

    }

    //called if player just started looking at this
    public void OnHoverEnter()
    {
        //N/A
    }

    //called if player just stopped looking at this 
    public void OnHoverExit()
    {
        //N/A
    }
	
}
