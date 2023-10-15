using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeImpactAudio : PlayerAudio
{
    // Start is called before the first frame update
    public static MeleeImpactAudio instance;

    private void Awake() {
        instance = this;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public override void PlayClip() {
        audioSource.Play();
    }
    
}