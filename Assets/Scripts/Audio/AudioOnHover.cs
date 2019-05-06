using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using VRStandardAssets.Utils;

public class AudioOnHover : MonoBehaviour { //could be done with inheritance

    protected AudioSource a_Effect;
    protected AudioMixer a_Mixer;

    public AudioClip touchClip;

    public bool playMe = false;

    void Start()
    {
        if (GetComponent<AudioSource>() == null)
        {
            a_Effect = gameObject.AddComponent<AudioSource>();
        }else
        {
            a_Effect = GetComponent<AudioSource>();
        }
        a_Effect.playOnAwake = false;
        a_Effect.loop = false;

        a_Mixer = Resources.Load("Mixer_Master") as AudioMixer;

        a_Effect.outputAudioMixerGroup = a_Mixer.FindMatchingGroups("GroupA")[0];
        GetComponent<VRInteractiveItem>().OnOver += InteractOnHover;
    }


    private void Update()
    {
        if (playMe)
        {
            InteractOnHover();
        }

    }

    // Update is called once per frame
    protected void InteractOnHover()
    {
        if(!a_Effect.isPlaying)
        {
            if(a_Effect.clip != touchClip || a_Effect.clip == null)
            {
                a_Effect.clip = touchClip;
            }
            a_Effect.loop = true;
            a_Effect.Play();
        }
    }
        
}
