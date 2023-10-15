using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAudio : PlayerAudio
{
    // Start is called before the first frame update
    public static PlayerMeleeAudio instance;

    private void Awake() {
        instance = this;
        audioSource = gameObject.GetComponent<AudioSource>();
    }
}
