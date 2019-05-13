using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pixelplacement;
using TMPro;

//class to control the tutorial
public class TuteController : MonoBehaviour {

    [Header("Tutorial Instructions and related objects")]
    public List<Instruction> tuteInstructions = new List<Instruction>();

    protected TextMeshPro textMesh;
    protected int listIndex = 0;

	// Use this for initialization
	void Start () {
        textMesh = GetComponent<TextMeshPro>();
        textMesh.text = tuteInstructions[listIndex].text; //the first set of instructions in the tutorial
        
       
	}

    //when the user completes the correct action, this function is called
    public void ProgressInstructions()
    {
        //deactivate the old stuff
        if(tuteInstructions[listIndex].linkedObject != null)
        {
            tuteInstructions[listIndex].linkedObject.SetActive(false);
        }
        listIndex++;
        //update objects
        textMesh.text = tuteInstructions[listIndex].text;
        if(tuteInstructions[listIndex].linkedObject != null)
        {
            tuteInstructions[listIndex].linkedObject.SetActive(true);
        }
    }

}

[System.Serializable]
public class Instruction
{
    [Tooltip("the text that will display for this instruction")]
    public string text;
    [Tooltip("the object this instruction turns on")]
    public DisplayObject linkedObject;
}
