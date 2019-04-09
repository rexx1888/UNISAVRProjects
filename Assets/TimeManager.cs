using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    public ScriptableObjectFloat timer;

	// Use this for initialization
	void Start () {
        timer.value = 0;
	}
	
	// Update is called once per frame
	void Update () {
        timer.value += Time.deltaTime;
	}
}
