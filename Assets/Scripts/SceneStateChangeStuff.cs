using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Pixelplacement;

[CreateAssetMenu(fileName = "sceneInformation_", menuName = "SceneSpecific/SceneInformation")]
public class SceneStateChangeStuff : ScriptableObject
{
    public VideoClip clip;
    public GameObject analyticTrackerObject;
    [HideInInspector]
    public DisplayObject spawnedAnalyticTracker;
}
