using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// 
/// Written by William Holman in October-November 2018.
/// 
/// this script is used to get the list of objects from the global state manager and fill out a text component with that list
/// 
/// </summary>

public class ObjectsInteractedWithListController : MonoBehaviour {

    public Text textToFill; //the text object to fill

    //on enable
    public void OnEnable()
    {
        //if the text object is not null
        if (textToFill != null)
        {
            //initial "list of items" phrase
            textToFill.text = "List Of Items Inspected: \n";
            //foreach container in the list
            foreach (StringAndIntContainer saic in GlobalStateManager.listOfObjectsAndAmountInspected)
            {
                //add the name and how many times its been inspected to the text field.
                textToFill.text += saic.nameOfObject + ": " + saic.amountInspeted + "\n";
            }
        }
    }
}
