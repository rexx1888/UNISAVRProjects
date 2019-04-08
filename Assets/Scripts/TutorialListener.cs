using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 
/// Written by William Holman in October-November 2018.
/// 
/// initially made for the tutorial level, forces player to interact with all objects before leaving
/// This listens for all the toggles in the list attached, and activates a canvas group object.
/// 
/// </summary>


public class TutorialListener : MonoBehaviour {

    public List<Toggle> listOfToggles = new List<Toggle>(); //list of toggles to listen for
    public float secondsToFadeContinueObject = 0.5f; //how long the fade should take

    public CanvasGroup continueButton; //the canvas group to enable/fade in 

    //every frame update
    public void Update()
    {
        //check if the tutorial is finished
        CheckIfTutorialFinished();
    }

    //check if all toggles are on
    public void CheckIfTutorialFinished()
    {
        //if the continue button is not already on
        if (!continueButton.gameObject.activeSelf)
        {
            //overarching checkedAll bool that gets set to false if one is unticked.
            bool checkedAll = true;
            //foreach toggle in the list
            foreach (Toggle tog in listOfToggles)
            {
                //if it is not on
                if (tog.isOn == false)
                {
                    //checked all is false
                    checkedAll = false;
                }
            }
            //if checked all is true, enable the object
            if (checkedAll)
            continueButton.gameObject.SetActive(checkedAll);
        }
    }
}
