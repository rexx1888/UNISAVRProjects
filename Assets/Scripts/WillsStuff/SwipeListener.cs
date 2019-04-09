using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

/// <summary>
/// 
/// Written by William Holman in October-November 2018.
/// 
/// This an object which uses the VRInput class from the camera, for documentation on what I've come to learn about how it works, check the programmer readme in the github repo.
/// 
/// This class is specifically designed for the tutorial level, to force the player to use the swipe functionality before being able to exit the inspection.
/// It will listen out for a swipe direction that isn't none, and when it happens it will fade in a canvas group button.
/// 
/// </summary>

public class SwipeListener : MonoBehaviour
{

    public CanvasGroup exitButtonToFade; //canvas group to fade in
    public float secondsToFade = 0.5f; //how long the fade should be

    //on start
    public void Start()
    {
        //get the vr input component from the camera
        VRInput vrInput = Camera.main.GetComponent<VRInput>();
        //if it is not null
        if (vrInput != null)
        {
            //add the function to the delegate
            vrInput.OnSwipe += FadeButton;
        }
    }

    //gets calle every frame by the onSwipe delegate
    public void FadeButton(VRInput.SwipeDirection swipeDir)
    {
        //if the swipe is not none, a swipe has actually happened
        if (swipeDir != VRInput.SwipeDirection.NONE)
        {
            //if the exit button is not null
            if (exitButtonToFade != null)
            {
                //if the exit button object is not active
                if (exitButtonToFade.gameObject.activeSelf == false)
                {
                    //set the object to be active, set the alpha to 0 and start the coroutine for fading it in.
                    exitButtonToFade.gameObject.SetActive(true);
                    exitButtonToFade.alpha = 0;
                    StartCoroutine(FadeLoadScreenToAlpha(exitButtonToFade, 1));
                }
            }
            //now get the vrInput component from the camera
            VRInput vrInput = Camera.main.GetComponent<VRInput>();
            //check if its null or not
            if (vrInput != null)
            {
                //remove the function from the delegate
                vrInput.OnSwipe -= FadeButton;
            }
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
