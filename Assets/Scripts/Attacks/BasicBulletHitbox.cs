using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBulletHitbox : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D col) {
        //Debug.Log("Triggered!");
        if (col.gameObject.tag == "PlayerHurtbox") {
            col.transform.parent.GetComponent<HealthScript>().TakeDamage(15f);
            gameObject.SetActive(false);
        }

        if (col.gameObject.tag == "BulletDestroyer") {
            GetComponent<Animator>().Play("BulletDestroy");
            
        }
    }

    void SetInactive() {
        gameObject.SetActive(false);
    }
}
