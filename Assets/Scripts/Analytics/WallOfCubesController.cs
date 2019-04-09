using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Written by William Holman in October-November 2018.
/// 
/// This object is intended for use in 'analytics'. kind of.
/// It has a list of objects attached, which make use of the TimeLookedAtObject script. it will enable/disable these objects when necessary.
/// 
/// Edited by David McCann(DESIGNER) in April 2019.
/// 
/// </summary>

public class WallOfCubesController : MonoBehaviour {

    private TimeLookedAtObject[] listOfTimedObjects;
    [HideInInspector]
    public GameObject objectsInteractedWithList;

    //on start
    public void Start()
    {
        //gets all the timelookedatobject componenets in this objects children
        listOfTimedObjects = this.GetComponentsInChildren<TimeLookedAtObject>();
    }

    //set the objects active state to be the passed in value
    public void EnableObjects(bool value)
    {
        //if the value is true
        if (value)
        {
            //change the game state to be finished
            GlobalStateManager.ChangeState(GameState.Finished);
            //if objectsInteractedWithList is not null
            if (objectsInteractedWithList != null)
            {
                //enable it
                objectsInteractedWithList.SetActive(true);
            }
        } else
        {
            //change the gamestate to be in game
            GlobalStateManager.ChangeState(GameState.InGame);
            //if the objectsInteractedWithList is not null
            if (objectsInteractedWithList != null)
            {
                //disable it 
                objectsInteractedWithList.SetActive(false);
            }
        }

        //if the listOfTimedObjects is not null and is more than 0
        if (listOfTimedObjects != null && listOfTimedObjects.Length > 0)
        {
            //foreach object
            foreach (TimeLookedAtObject obj in listOfTimedObjects)
            {
                //turn the meshrenderer on, so its visible now.
                MeshRenderer rend = obj.GetComponent<MeshRenderer>();
                if (rend != null)
                {
                    rend.enabled = value;
                }
            }
        }
    }
}
