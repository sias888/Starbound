using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{

    public Animator anim;
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "PlayerHurtbox") {
            col.transform.parent.gameObject.SetActive(false);
            anim.SetTrigger("Despawn");
        }
    }
}
