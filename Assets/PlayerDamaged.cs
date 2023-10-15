using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamaged : PlayerAudio
{
    // Start is called before the first frame update
    public static PlayerDamaged instance;

    private void Awake() {
        instance = this;
        audioSource = gameObject.GetComponent<AudioSource>();
    }
}