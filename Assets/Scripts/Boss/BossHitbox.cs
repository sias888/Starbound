using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitbox : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D col) {
        if (col.gameObject.tag == "PlayerHurtbox") {
            col.transform.parent.GetComponent<HealthScript>().TakeDamage(20);
        } 
    }
}
