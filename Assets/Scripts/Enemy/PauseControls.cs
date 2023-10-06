using System.Collections;
using System.Collections.Generic;
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause() {
        PlayerScript.instance.enabled = false;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume() {
        Time.timeScale = 1f;
        PlayerScript.instance.enabled = true;
        pauseMenu.SetActive(false);
        isPaused = false;
        PlayerInput.instance.SetDodgePressed(false);
    }


    public void HandlePauseEvent() {
        if (isPaused) {
            Resume();
        } else {
            Pause();
        }
    }
}
