using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuControls : MonoBehaviour
{
    public GameObject pauseController;
    public void MainMenu() {
        Time.timeScale = 1f;
        Invoke("LoadScene", 0.1f);
        PlayerScript.instance.enabled = true;
        PauseControls.isPaused = false;
        PlayerInput.instance.SetDodgePressed(false);
    }

    void LoadScene() {
        SceneManager.LoadScene("Level1");
    }

    public void Resume() {
        pauseController.GetComponent<PauseControls>().Resume();
    }

    public void Options() {

    }

    public void QuitGame() {
        //Debug.Log("QUIT!");
        Application.Quit();
    }
}
