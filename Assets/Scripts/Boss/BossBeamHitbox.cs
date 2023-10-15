using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBeamHitbox : MonoBehaviour
{
    public AudioSource audioSource;
    public Animator anim;

    List<GameObject> beams;
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        beams = BeamStartAudio.instance.beams;
    }
    void OnTriggerEnter2D(Collider2D col) {
        //Debug.Log("Triggered!");
        if (col.gameObject.tag == "PlayerHurtbox") {
            col.transform.parent.GetComponent<HealthScript>().TakeDamage(40f);
        } 
    }

    private void OnEnable() {
        Invoke("Destroy", 60f);
        beams.Add(gameObject);

        audioSource.Play();

        if (beamsHasAtLeast1MakingSound()) audioSource.mute = true;
        else audioSource.mute = false;
    }


    private bool beamsHasAtLeast1MakingSound()
    {
        foreach(GameObject beam in beams) {
            if (beam) {
                if (beam.activeInHierarchy) {
                    if (!beam.GetComponent<AudioSource>().mute) {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    private GameObject returnFirstBeam()
    {
        foreach(GameObject beam in beams) {
            if (beam) {
                if (beam.activeInHierarchy) {
                    if (!beam.GetComponent<AudioSource>().mute) {
                        return beam;
                    }
                }
            }
        }

        foreach(GameObject beam in beams) {
            if (beam) {
                if (beam.activeInHierarchy) {
                    return beam;
                }
            }
        }

        return null;
    }

    private void Destroy() {
        StopClip();
        gameObject.SetActive(false);
    }

    private void OnDisable() {
        StopClip();
        CancelInvoke();
    }

    public void StopClip() {
      BeamStartAudio.instance.StopClip();

      audioSource.Stop();
    }

    public void StopNotification() {
        if (beamsHasAtLeast1MakingSound()) audioSource.mute = true;
        else audioSource.mute = false;

        if (returnFirstBeam())
            returnFirstBeam().GetComponent<AudioSource>().mute = false;
    }
    
}
