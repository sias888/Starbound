using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAudio : PlayerAudio
{
    // Start is called before the first frame update
    public static HealAudio instance;

    private void Awake() {
        instance = this;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void StartClip() {
        if (!audioSource.isPlaying) audioSource.Play();
    }

    public void StopClip() {
        if (audioSource.isPlaying) audioSource.Stop();
    }
}