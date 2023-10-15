using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamStartAudio : PlayerAudio
{

    public static BeamStartAudio instance;

    private void Awake() {
        instance = this;
        audioSource = gameObject.GetComponent<AudioSource>();
        beams = new List<GameObject>();
    }

    override public void PlayClip() {
        
        Debug.Log("PlayStart!");
        //BeamLoopAudio.instance.PlayClip();
        StartCoroutine(PlayLoopedSegment());
    }

    public void PlayClip(bool b) {
        audioSource.Play();
    }

    IEnumerator PlayLoopedSegment() {
        //BeamLoopAudio.instance.PlayClip();    
        yield return new WaitForEndOfFrame();//audioSource.clip.length);
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        BeamLoopAudio.instance.PlayClip();
    }

    public List<GameObject> beams;
    public void StopClip() {
        foreach (GameObject beam in beams) {
            if (beam) {
                if (beam.activeInHierarchy)
                    beam.GetComponent<BossBeamHitbox>().StopNotification();
            }
        }

        if (!checkBeams())
            if (audioSource) {
                if (audioSource.isPlaying) audioSource.Stop();
                BeamLoopAudio.instance.StopClip();
            }
    }

    private bool checkBeams()
    {
        foreach (GameObject beam in beams) {
            if (beam) {
                if (beam.activeInHierarchy)
                    return true;
            }
        }
        return false;
    }
}
