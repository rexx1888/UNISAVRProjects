using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Pixelplacement;

[CreateAssetMenu(fileName ="currentState_", menuName = "SceneSpecific/CurrentSceneStorage")]
public class CurrentSceneState : ScriptableObject {

    public List<SceneStateChangeStuff> sceneStuff = new List<SceneStateChangeStuff>();
    public SceneStateChangeStuff currentScene;
}



