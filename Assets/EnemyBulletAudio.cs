using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletAudio : PlayerAudio
{

    public static EnemyBulletAudio instance;

    private void Awake() {
        instance = this;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    override public void PlayClip() {
        audioSource.Play();
    }
}
