using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using VRStandardAssets.Utils;
using Pixelplacement;
using TMPro;

//class to control the tutorial
//this was made in the last day of dev for this project. Expect a mess - David McCann
public class TuteController : MonoBehaviour {

    [Header("Tutorial Instructions and related objects")]
    public List<Instruction> tuteInstructions = new List<Instruction>();

    [Header("reference to the Interactive Item Controller")]
    [SerializeField] protected VRInput c_Input;

    protected TextMeshPro textMesh;
    protected int listIndex = 0;
    protected bool canvasDisplayed = false;

    // Use this for initialization
    void Start () {
        textMesh = GetComponent<TextMeshPro>();
        textMesh.text = tuteInstructions[listIndex].text; //the first set of instructions in the tutorial


        c_Input.OnHold += FirstStep;
    }
  
    //this is hacky but also tme constraints on Tutorial creation
    public void FirstStep()
    {
        if(!canvasDisplayed)
        {
            canvasDisplayed = true;
            ProgressInstructions();
        }
    }

    //when the user completes the correct action, this function is called
    public void ProgressInstructions()
    {

        listIndex++;
        //update objects
        textMesh.text = tuteInstructions[listIndex].text;

    }

}

[System.Serializable]
public class Instruction
{
    [Tooltip("the text that will display for this instruction")]
    public string text;
    [Tooltip("the object this instruction turns on")]
    public GameObject linkedObject;
    [HideInInspector]
    public DisplayObject display;
}
