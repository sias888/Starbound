using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{

    public GameObject ScoreScreen;
    public Animator anim;
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "PlayerHurtbox") {
            TeleportAudio.instance.PlayClip();
            col.transform.parent.GetChild(0).gameObject.SetActive(false);
            PlayerScript.instance.enabled = false;
            anim.SetTrigger("Despawn");
            //Invoke("SpawnScore", 0.5f);
        }
    }

    public void SpawnScore(float f) {
        StartCoroutine(SS(f));
    }

    IEnumerator SS(float f) {
       yield return new WaitForSeconds(f);
        ScoreScreen.SetActive(true);
        gameObject.SetActive(false);
    }
}
