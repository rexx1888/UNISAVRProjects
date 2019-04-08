using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Written by William Holman in October-November 2018.
/// 
/// This classed is used for spawning canvases and inspectable objects in front of the camera.
/// 
/// </summary>



public class CanvasController : MonoBehaviour {

    public List<StateCanvasGrouping> stateCanvasGroupings = new List<StateCanvasGrouping>(); //container class for each canvas group with state and spawned object
    public float distToSpawnCanvas = 10; //how far from the camera to spawn the canvases
    public GameObject spawnedInspectedObject; //the gameobject that you are inspecting that is spawned
    public float distToSpawnInspectObject = 5; //how far from the camera to spawn the inspected object
    public float secondsToFade = 0.5f; //how long the canvases should fade

    //when this object is enabled
    public void OnEnable()
    {
        //GlobalStateManager.onStateChanged += StateUpdated;
        foreach (StateCanvasGrouping scg in stateCanvasGroupings)
        {

            scg.spawnedCanvas = null;
        }
    }

    //Used to be called when the state was updated on the globalstatemanager to change canvases, is deprecated.

    /*
    public void StateUpdated(GameState newState)
    {
        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0;
        camForward.Normalize();
        camForward *= distToSpawnCanvas;

        foreach (StateCanvasGrouping scg in stateCanvasGroupings)
        {

            if (scg.stateRequired == newState)
            {
                if (scg.canvasToSpawn != null)
                {


                    if (scg.spawnedCanvas != null)
                    {
                        Destroy(scg.spawnedCanvas);
                        scg.spawnedCanvas = null;
                    }
                    scg.spawnedCanvas = GameObject.Instantiate(scg.canvasToSpawn, this.transform.position + camForward, Quaternion.identity);
                    scg.spawnedCanvas.transform.LookAt(Camera.main.transform);
                    scg.spawnedCanvas.transform.Rotate(new Vector3(0, 180, 0));
                }
            } else
            {
                if (scg.spawnedCanvas != null)
                {
                    Destroy(scg.spawnedCanvas);
                    scg.spawnedCanvas = null;
                }
            }
        }
    }
    */

        //Delete the spawned object if it exists
    public void DeleteObject()
    {
        //if the spawned object exists
        if (spawnedInspectedObject != null)
        {
            //destroy it
            Destroy(spawnedInspectedObject);
        }
    }

    //spawn an object at position
    public void SpawnObj(GameObject objToSpawn, Vector3 posToSpawn)
    {
        //if the object is not null
        if (objToSpawn != null)
        {
            //if the spawned object exists already
            if (spawnedInspectedObject != null)
            {
                //destroy it. set it to null.
                Destroy(spawnedInspectedObject);
                spawnedInspectedObject = null;
            }
            //create the new object at position.
            spawnedInspectedObject = GameObject.Instantiate(objToSpawn, posToSpawn, Quaternion.identity);
        }
    }

    //Change the canvas that is spawned in front of the camera, and a new inspectable object and state.
    public void ChangeCanvasAndState(GameObject fadeOutee, GameObject fadeInee, GameState stateToChangeToAfter, GameObject newInspectObj)
    {
        //start a coroutine to do it over multiple frames, so its not instant.
        StartCoroutine(FadeBetweenCanvas(fadeOutee, fadeInee, stateToChangeToAfter, newInspectObj));
    }

    //fade between two objects, change the game state and spawn a new inspected objet
    public IEnumerator FadeBetweenCanvas(GameObject fadeOutee, GameObject fadeInee, GameState stateToChangeToAfter, GameObject newInspectObj)
    {
        //change the global state to be the new state
        GlobalStateManager.ChangeState(stateToChangeToAfter);


        //if the object to fade out is not null
        if (fadeOutee != null)
        {
            //try to get a canvas group
            CanvasGroup fadeOuteeCG = fadeOutee.GetComponent<CanvasGroup>();

            //if the canvas group component exists
            if (fadeOuteeCG != null)
            {
                //wait until this object is completely faded out
                yield return StartCoroutine(FadeLoadScreenToAlpha(fadeOuteeCG, 0));
            }
            //disable it
            fadeOutee.SetActive(false);
        }
        //if the game state is no longer inspecting, we can delete the inspected object
        if (stateToChangeToAfter != GameState.Inspecting)
        {
            DeleteObject();
        }

        //create a temporary fade in spawn object
        GameObject fadeIneeSpawn = null;
        //if the object to fade in is not null
        if (fadeInee != null)
        {
            //if the fade out object is not null
            if (fadeOutee != null)
            {
                //get the position of this object to spawn the new canvas at, and create it there.
                fadeIneeSpawn = GameObject.Instantiate(fadeInee, fadeOutee.transform.position, Quaternion.identity);
                //make it look at the camera
                fadeIneeSpawn.transform.LookAt(Camera.main.transform);
                //rotate it, since look at is backwards for some reason?
                fadeIneeSpawn.transform.Rotate(new Vector3(0, 180, 0));
            } else //there was no object to fade out in the first place, so just create the new one
            {
                //get the position in front of the camera, and distance it from the camera by the distToSpawnCanvas variable
                Vector3 camForward = Camera.main.transform.forward;
                camForward.y = 0;
                camForward.Normalize();
                camForward *= distToSpawnCanvas;

                //create the new object at the above position in front of the camera, make it look at the camera
                fadeIneeSpawn = GameObject.Instantiate(fadeInee, this.transform.position + camForward, Quaternion.identity);
                //make it look at the camera
                fadeIneeSpawn.transform.LookAt(Camera.main.transform);
                //rotate it, since look at is backwards for some reason
                fadeIneeSpawn.transform.Rotate(new Vector3(0, 180, 0));
            }
            //if the fade in spawn is not null
            if (fadeIneeSpawn != null)
            {
                //try to get a canvas group from it
                CanvasGroup fadeIneeCG = fadeIneeSpawn.GetComponent<CanvasGroup>();
                //if the canvas group exists
                if (fadeIneeCG != null)
                {
                    //set the alpha to 0
                    fadeIneeCG.alpha = 0;
                    //then fade the object in, wait until it is done fading
                    yield return StartCoroutine(FadeLoadScreenToAlpha(fadeIneeCG, 1));
                }
            }
        }

        //if the new state is inspecting
        if (stateToChangeToAfter == GameState.Inspecting)
        {
            //make a new position to spawn the object
            Vector3 newPos = Vector3.zero;
            //if the fade in spawn object exists
            if (fadeIneeSpawn != null)
            {
                //get the position of the fade in spawn object
                newPos = fadeIneeSpawn.transform.position;
                //get the direction of that object to the camera (this object)
                Vector3 heading = this.gameObject.transform.position - newPos;
                //normalise it
                heading.Normalize();
                //multiply it by the distance left over, which is how far it needs to be from the camera.
                heading *= distToSpawnCanvas - distToSpawnInspectObject;
                //new pos += the heading, which makes it spawn at the position intended
                newPos += heading;
            } 
            //create this new object that that position
            SpawnObj(newInspectObj, newPos);
        }

    }

    //fade a canvas group's alpha to the passed in alpha
    public IEnumerator FadeLoadScreenToAlpha(CanvasGroup canvasToFade, float newAlpha)
    {
        
        //get the starting alpha and set a new timer
        float elapsedTime = 0;
        float startingAlpha = canvasToFade.alpha;

        //while this timer is less than the seconds to fade 
        while (elapsedTime <= secondsToFade)
        {
            //add to elapsed time
            elapsedTime += Time.deltaTime;
            //lerp the amount between the starting alpha and the new alpha by how far along the fade duration is
            float lerpAmount = Mathf.Lerp(startingAlpha, newAlpha, elapsedTime / secondsToFade);
            //set the alpha
            canvasToFade.alpha = lerpAmount;

            //repeat until finished
            yield return null;
        }
    }  
}
