using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class DeadController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject RetryButton;
    public GameObject EventSystemGameObject;
    void Start()
    {
        EventSystemGameObject.GetComponent<EventSystem>().SetSelectedGameObject(RetryButton);
        StartCoroutine(CanSelectTrue());
    }

    bool canSelect = false;

    IEnumerator CanSelectTrue() {
        yield return new WaitForSecondsRealtime(0.25f);
        canSelect = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Retry() {
        if (canSelect) {
        //this.transform.parent.gameObject.SetActive(false);
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void MainMenu() {
        if (canSelect) {
            Time.timeScale = 1f;
            Invoke("LoadScene", 0.1f);
            PlayerScript.instance.enabled = true;
            PauseControls.isPaused = false;
            PlayerInput.instance.SetDodgePressed(false);
        }
    }
    void LoadScene() {
        SceneManager.LoadScene("Level1");
    }
    public void QuitGame() {
        if (canSelect)
            Application.Quit();
    }
}
