using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    protected AudioSource audioSource;
    public virtual void PlayClip() {
        if (audioSource.isPlaying) audioSource.Stop();
        audioSource.Play();
    }
}
