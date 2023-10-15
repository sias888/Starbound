using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public class PauseControls : MonoBehaviour
{
    public GameObject pauseMenu;

    public static bool isPaused = false;


    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }


    public void Pause() {
        BeamLoopAudio.instance.PauseClip();
        BeamPauseMethod();
        PlayerInput.instance.EnableDodge(false);
        PlayerScript.instance.enabled = false;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    private void BeamPauseMethod()
    {
        GameObject[] beams = GameObject.FindGameObjectsWithTag("Beam");

        foreach(GameObject beam in beams) {
            if (beam) {
                if (beam.activeInHierarchy) {
                    beam.GetComponentInChildren<AudioSource>().Pause();
                }
            }
        }
    }

    private void BeamUnPauseMethod()
    {
        GameObject[] beams = GameObject.FindGameObjectsWithTag("Beam");

        foreach(GameObject beam in beams) {
            if (beam) {
                if (beam.activeInHierarchy) {
                    beam.GetComponentInChildren<AudioSource>().UnPause();
                }
            }
        }
    }

    public void Resume() {
        BeamLoopAudio.instance.UnpauseClip();
        BeamUnPauseMethod();
        Time.timeScale = 1f;
        PlayerScript.instance.enabled = true;
        pauseMenu.SetActive(false);
        PlayerInput.instance.EnableDodge(true);
        PlayerInput.instance.SetDodgePressed(false);
        isPaused = false;
    }


    public void HandlePauseEvent() {
        if (isPaused) {
            Resume();
        } else {
            Pause();
        }
    }
}
