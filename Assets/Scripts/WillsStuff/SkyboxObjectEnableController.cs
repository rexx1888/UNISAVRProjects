using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// Written by William Holman in October-November 2018.
/// 
/// this object is for controlling the base objects for each skybox dependent object, and enabling/disabling them when needed
/// 
/// </summary>

public class SkyboxObjectEnableController : MonoBehaviour {

    public List<GameObject> baseObjectList; //the list of skybox dependent objects

    //enables the object passed in if its in the list, and disables all others in the list that are not that one
    public void EnableObject(GameObject objToEnable)
    {

        //foreach gameobject in the list
        foreach (GameObject go in baseObjectList)
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
}
