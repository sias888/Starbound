using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitbox : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {
        //Debug.Log("Triggered!");
        if (col.gameObject.tag == "PlayerHurtbox") {
            col.transform.parent.GetComponent<HealthScript>().TakeDamage(15f);
            gameObject.SetActive(false);
        } 
    }
}
