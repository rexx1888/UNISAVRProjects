using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.UI;
using Pixelplacement;
using TMPro;

[RequireComponent(typeof(VRInteractiveItem))]
public class LoadAnalyticData : MonoBehaviour, IInteractable
{
    public Analytics analytics; //link to the analytics scriptable object
    public string index;
    public DisplayObject controlButtons;
    public DisplayObject menu;
    //public WallOfCubesController wallController; //the controller for the walls surrounding the camera object. 
    private VRInteractiveItem vrII; //the VRInteractiveItem attached to this.
    public SimpleObjectPool pool;


    //on start
    public void Start()
    {
        //get the vrInteractiveItem component attached to this
        vrII = this.GetComponent<VRInteractiveItem>();
        //safety check
        if (vrII != null)
        {
            //assign the functions to the delegates
            vrII.OnOver += OnHoverEnter;
            vrII.OnOut += OnHoverExit;
            vrII.OnClick += Interact;
        }
        pool = GameObject.FindGameObjectWithTag("ObjectPoolButtons").GetComponent<SimpleObjectPool>();
    }

    public void SetName(string name)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = name;
    }

    //when this object is clicked on
    public void Interact()
    {
        analytics.LoadData(index);
        pool.ReturnAllObjects();
        controlButtons.SetActive(true);
        menu.SetActive(false);
    }


    //when this object is looked at
    public void OnHoverEnter()
    {

    }

    //when this object is no longer looked at
    public void OnHoverExit()
    {

    }

}
