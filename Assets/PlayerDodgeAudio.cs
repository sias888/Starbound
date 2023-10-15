using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeAudio : PlayerAudio
{
    // Start is called before the first frame update
    public static PlayerDodgeAudio instance;

    private void Awake() {
        instance = this;
        audioSource = gameObject.GetComponent<AudioSource>();
    }
}