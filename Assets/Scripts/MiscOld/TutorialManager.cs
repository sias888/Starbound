using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] UIPopups;
    private int UIPIndex;

    private float WaitTime = 1.2f;

    public GameObject Teleporter;

    //float wt;

    bool canAdvance;

    void Awake() {
        UIPIndex = 0;
        //wt = WaitTime;
        canAdvance = true;
    }

    IEnumerator Advance() {
        canAdvance = false;
        yield return new WaitForSeconds(WaitTime);
        canAdvance = true;
        UIPIndex++;
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < UIPopups.Length; i++) {
            if (i == UIPIndex) {
                UIPopups[i].SetActive(true);
            } else {
                UIPopups[i].SetActive(false);
            }
        }
        

        if (UIPIndex == 0) {
            if (Input.GetAxisRaw("Horizontal") != 0 ||Input.GetAxisRaw("Vertical") != 0) {
                if (canAdvance) StartCoroutine(Advance());
            }
        }

        if (UIPIndex == 1) {
            if (Input.GetKey(KeyCode.LeftShift)) {
                if (canAdvance) StartCoroutine(Advance());
            }
        }

        if (UIPIndex == 2) {
            if (Input.GetButtonDown("Jump")) {
                if (canAdvance) StartCoroutine(Advance());
            }
        }

        if (UIPIndex == 3) {
            if (Input.GetButtonDown("Melee")) {
                if (canAdvance) StartCoroutine(Advance());
            }
        }

        if (UIPIndex == 4) {
            if (Input.GetButtonDown("Heal")) {
                if (canAdvance) {
                    StartCoroutine(Advance());
                    Teleporter.SetActive(true);
                }
            }
        }

        if (HealthScript.instance.GetHealth() <= 20) {
            HealthScript.instance.Heal(100);
        }

    }
}
