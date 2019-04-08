using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

/// <summary>
/// 
/// Written by William Holman in October-November 2018.
/// 
/// This an object which uses the VRInput class from the camera, for documentation on what I've come to learn about how it works, check the programmer readme in the github repo.
/// This script is for when an object is being inspected, they can be spun around by the player using the touch pad.
/// 
/// 
/// </summary>

public class Spin : MonoBehaviour {

    private Vector2 rotateVelocity; //the rotate velocity
    public float rotationMultiplier = 16; // rotation speed
    public float rotationFriction = 0.9f; //the rotate velocity is multiplied by this every frame, so the value decreases over time.
    public float swipeAmountMultiplier = 275f; //multiplier for swipe amount, required to be large to work properly.

    //on enable
    public void OnEnable()
    {
        //get the vrInput from the main camera
        VRInput vrInput = Camera.main.GetComponent<VRInput>();
        //if it exists
        if (vrInput != null)
        {
            //add the function to the delegate
            vrInput.OnSwipe += DoSpin;
        }

    }

    //called every physics frame
    public void FixedUpdate()
    {
        //apply friction to the inspected object.
        ApplyFriction();
        //rotate the object in world space using rotate velocity.
        this.transform.Rotate(rotateVelocity * Time.deltaTime, Space.World);
    }


    //called every frame by the vrInput on the camera.
    public void DoSpin(VRInput.SwipeDirection swipeDirection)
    {
        //swipe multiplier
        float swipeFloat  = 1* swipeAmountMultiplier;

        //switch statement for which type of swipe it was
        switch (swipeDirection)
        {
            case VRInput.SwipeDirection.NONE:
                {
                    //if the swipe direction was none, there wasn't actually a swipe. It gets called every frame regardless.
                    //therefore return and don't do anything else.

                    return;
                }
            case VRInput.SwipeDirection.UP:
                {
                    //add to the rotate velocity by the objects x value by the swipe multiplier
                    rotateVelocity.x += swipeFloat;
                    break;
                }
            case VRInput.SwipeDirection.DOWN:
                {
                    //subtract the rotate velocity by the objects x value by the swipe multiplier
                    rotateVelocity.x += -swipeFloat;
                    break;
                }
            case VRInput.SwipeDirection.LEFT:
                {
                    //add to the rotate velocity by the objects y value by the swipe multiplier
                    rotateVelocity.y += swipeFloat;
                    break;
                }
            case VRInput.SwipeDirection.RIGHT:
                {
                    //subtract to the rotate velocity by the objects y value by the swipe multiplier
                    rotateVelocity.y += -swipeFloat;
                    break;
                }
                //if facing the touch pad
                //right = west
                //left = east
                //down = south
                //up = north
            case VRInput.SwipeDirection.SouthWestDiagonal:
                {
                    //add to the rotation velocity in the specified direction
                    rotateVelocity.y += -swipeFloat;
                    rotateVelocity.x += -swipeFloat;
                    break;
                }
            case VRInput.SwipeDirection.SouthEastDiagonal:
                {
                    //add to the rotation velocity in the specified direction
                    rotateVelocity.y += swipeFloat;
                    rotateVelocity.x += -swipeFloat;
                    break;
                }
            case VRInput.SwipeDirection.NorthEastDiagonal:
                {
                    //add to the rotation velocity in the specified direction
                    rotateVelocity.y += swipeFloat;
                    rotateVelocity.x += swipeFloat;
                    break;
                }
            case VRInput.SwipeDirection.NorthWestDiagonal:
                {
                    //add to the rotation velocity in the specified direction
                    rotateVelocity.y += -swipeFloat;
                    rotateVelocity.x += swipeFloat;
                    break;
                }
        }

    }

    //apply friction to the rotation velocity
    public void ApplyFriction()
    {
        //multiply the rotate velocity by the friction value (should be less than 1, otherwise it will increas the velocity
        rotateVelocity.x *= rotationFriction;
        rotateVelocity.y *= rotationFriction;

    }
}
