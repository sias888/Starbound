using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillAudio : PlayerAudio
{
    // Start is called before the first frame update
    public static EnemyKillAudio instance;

    private void Awake() {
        instance = this;
        audioSource = gameObject.GetComponent<AudioSource>();
    }
}