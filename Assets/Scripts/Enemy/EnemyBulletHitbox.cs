using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletHitbox : MonoBehaviour
{

    bool CanHit;
    bool isDeflected = false;
    // Start is called before the first frame update
    void Start()
    {
        CanHit = true;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "PlayerHurtbox") {
            if (CanHit) {
                col.transform.parent.GetComponent<HealthScript>().TakeDamage(20f);
                Destroy(transform.parent.gameObject);
            }
        }

        if (col.gameObject.tag == "Enemies" && isDeflected) {
            col.gameObject.GetComponent<Enemy>().TakeDamage(10f);
            Destroy(transform.parent.gameObject, 0.1f);
        }
    }

    public void Deflect() {
        if (!isDeflected) {
            isDeflected = true;
            transform.parent.GetComponent<EnemyBulletScript>().Deflect();
        }
    }
}
