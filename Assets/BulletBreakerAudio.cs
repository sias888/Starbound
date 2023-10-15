using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBreakerAudio : PlayerAudio
{
    // Start is called before the first frame update
    public static BulletBreakerAudio instance;

    private void Awake() {
        instance = this;
        audioSource = gameObject.GetComponent<AudioSource>();
    }
}