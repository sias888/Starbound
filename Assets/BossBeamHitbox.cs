using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBeamHitbox : MonoBehaviour
{
    public Animator anim;
    void OnTriggerEnter2D(Collider2D col) {
        //Debug.Log("Triggered!");
        if (col.gameObject.tag == "PlayerHurtbox") {
            col.transform.parent.GetComponent<HealthScript>().TakeDamage(39f);
        } 
    }

    private void OnEnable() {
        Invoke("Destroy", 7f);
    }

    private void Destroy() {
        gameObject.SetActive(false);
    }

    private void OnDisable() {
        CancelInvoke();
    }
}
