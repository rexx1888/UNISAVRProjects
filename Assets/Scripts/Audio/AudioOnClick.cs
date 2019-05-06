using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using VRStandardAssets.Utils;

public class AudioOnClick : MonoBehaviour {

    protected AudioSource a_Effect;
    protected AudioMixer a_Mixer;

    public AudioClip touchClip;

    public bool playMe = false;

    void Start () {
        if (GetComponent<AudioSource>() == null)
        {
            a_Effect = gameObject.AddComponent<AudioSource>();
        }
        a_Effect.playOnAwake = false;
        a_Effect.loop = false;

        a_Mixer = Resources.Load("Mixer_Master") as AudioMixer;
        
        a_Effect.outputAudioMixerGroup = a_Mixer.FindMatchingGroups("GroupA")[0];
        GetComponent<VRInteractiveItem>().OnTouch += InteractTouch;
	}

    private void Update()
    {
        if(playMe)
        {
            InteractTouch();
        }

    }

    void InteractTouch () {
        a_Effect.loop = false;
        a_Effect.PlayOneShot(touchClip);
	}
}
