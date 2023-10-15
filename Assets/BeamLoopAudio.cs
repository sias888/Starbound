using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamLoopAudio : PlayerAudio
{

    public static BeamLoopAudio instance;

    private void Awake() {
        instance = this;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    override public void PlayClip() {
        audioSource.Play();
    }

    public void StopClip() {
        if (audioSource)
            if (audioSource.isPlaying) audioSource.Stop();
    }

    bool Paused = false;
    public void PauseClip() {
        if (audioSource) {
            if (audioSource.isPlaying) audioSource.Pause();
            Paused = true;
        }
    }
    
    public void UnpauseClip() {
        if (audioSource) {
            if (Paused){
                 audioSource.UnPause();
                 Paused = false;
            }
        }
    }

    public void Mute(bool b) {
        //audioSource.mute = b;
    }
}