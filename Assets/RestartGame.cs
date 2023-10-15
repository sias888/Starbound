using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    void Restart() {
        Time.timeScale = 1f;
        Invoke("LoadScene", 0.1f);
        PlayerScript.instance.enabled = true;
        PauseControls.isPaused = false;
        PlayerInput.instance.SetDodgePressed(false);
    }

    void LoadScene() {
        SceneManager.LoadScene("Level1");
    }

    private void Awake() {
        //gameObject.GetComponent<Button>().
    }
}
