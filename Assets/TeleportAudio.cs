using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAudio : PlayerAudio
{
    // Start is called before the first frame update
    public static TeleportAudio instance;

    private void Awake() {
        instance = this;
        audioSource = gameObject.GetComponent<AudioSource>();
    }
}