using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseTPAudio : PlayerAudio
{
    // Start is called before the first frame update
    public static ReverseTPAudio instance;

    private void Awake() {
        instance = this;
        audioSource = gameObject.GetComponent<AudioSource>();
    }
}