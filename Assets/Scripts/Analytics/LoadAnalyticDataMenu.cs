using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using UnityEngine.UI;
using Pixelplacement;
using System.IO;

[RequireComponent(typeof(VRInteractiveItem))]
public class LoadAnalyticDataMenu : MonoBehaviour, IInteractable
{
    //button setters
    public DisplayObject loadmenu;
    public DisplayObject buttons;
    public Analytics analytics; //link to the analytics scriptable object

    public SimpleObjectPool pool;
    public Transform contentPanel;
    public List<FileInfo> loadIndices = new List<FileInfo>();
    protected string sessionDataProjectFilePath = "/StreamingAssets";
	public float userInterfaceDistance;

	//public WallOfCubesController wallController; //the controller for the walls surrounding the camera object. 
	private VRInteractiveItem vrII; //the VRInteractiveItem attached to this.

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

        gameObject.AddComponent<DisplayObject>().SetActive(true);

    }

    //when this object is clicked on
    public void Interact()
    {
        buttons.SetActive(false);
		loadmenu.transform.position = Camera.main.transform.forward * userInterfaceDistance; //not finished yet
		loadmenu.SetActive(true);

		PopulateLoadButtons();

		//direction * distance + sighting position
	}


    //when this object is looked at
    public void OnHoverEnter()
    {

    }

    //when this object is no longer looked at
    public void OnHoverExit()
    {

    }

    public void PopulateLoadButtons()
    {
        DirectoryInfo d = new DirectoryInfo(Application.dataPath + sessionDataProjectFilePath);
        FileInfo[] fis = d.GetFiles();
        if (fis.Length > 0)
        {
            foreach (FileInfo f in fis)
            {
                GameObject newButton = pool.GetObject();
                newButton.transform.SetParent(contentPanel);
				newButton.transform.localPosition = Vector3.zero;
                LoadAnalyticData buttonScript = newButton.GetComponent<LoadAnalyticData>();
                buttonScript.index = Path.Combine(Application.streamingAssetsPath, f.Name);
                buttonScript.menu = loadmenu;
                buttonScript.controlButtons = buttons;
                buttonScript.analytics = analytics;
            }
        }
    }

}

