using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Written by William Holman in October-November 2018.
/// 
/// This is a container class that is used in the Canvas Controller class.
/// 
/// 
/// </summary>

[System.Serializable]
public class StateCanvasGrouping {

    public GameObject canvasToSpawn; //the object (specifically designed to be a canvas) that will spawn
    [HideInInspector] public GameObject spawnedCanvas; //the spawned object (intended to be canvas) 
    public GameState stateRequired; //state required for this container to activate

}
