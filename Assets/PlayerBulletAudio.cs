using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletAudio : PlayerAudio
{

    public static PlayerBulletAudio instance;

    private void Awake() {
        instance = this;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    override public void PlayClip() {
        audioSource.Play();
    }
}
