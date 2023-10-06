using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public GameObject Multiplier;
    public GameObject Score;
    public GameObject Health;
    public GameObject Energy;

    public GameObject LevelController;
    public GameObject ScoreManager;
    public GameObject BulletBreakers;

    //bool canStart = false;
    public bool canStart = true;
    void SetCanStartTrue() {
        canStart = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Invoke("SetCanStartTrue", 0.25f);
        Multiplier.SetActive(false);
        Score.SetActive(false);
        Health.SetActive(false);
        Energy.SetActive(false);
        LevelController.GetComponent<Level1EnemyController>().start = false;
        ScoreManager.SetActive(false);
        BulletBreakers.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (pressAButtonLock) StartCoroutine(PressAButtonAnimator());
    }

    bool pressAButtonLock = true;
    List<String> pressAButtonTexts = new List<string> {
        "Press A Button.",
        "Press A Button..",
        "Press A Button..."
    };
    
    String pressAButton;
    int i = 0;
    IEnumerator PressAButtonAnimator() {
        pressAButtonLock = false;
        pressAButton = pressAButtonTexts[i%pressAButtonTexts.Count];
        i++;
        transform.GetChild(0).GetChild(1).GetComponentInChildren<TMP_Text>().SetText(pressAButton);
        yield return new WaitForSeconds(0.5f);
        pressAButtonLock = true;
    }

    public void StartGame() {
        if (canStart) {
            Multiplier.SetActive(true);
            Score.SetActive(true);
            Health.SetActive(true);
            Energy.SetActive(true);
            Invoke("StartEnemies", 1f);
            ScoreManager.SetActive(true);
            transform.GetChild(0).gameObject.SetActive(false);
            BulletBreakers.SetActive(true);
        }
        canStart = false;
    }

    void StartEnemies() {
        LevelController.GetComponent<Level1EnemyController>().start = true;
    }
}
