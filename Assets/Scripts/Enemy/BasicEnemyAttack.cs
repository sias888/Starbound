using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAttack : MonoBehaviour
{
   Rigidbody2D myRigidBody;

    List<GameObject> enemiesAlreadyHit = new List<GameObject>();

    bool CanShoot;

    public GameObject enemyBullet;
    public Transform firePoint;
    GameObject enemyBulletCopy;



    // Start is called before the first frame update
    void Start()
    {
        CanShoot = true;
    }


    void OnTriggerEnter2D(Collider2D col) {
        //Debug.Log("Triggered!");
        if (col.gameObject.tag == "PlayerHurtbox") {
            col.transform.parent.GetComponent<HealthScript>().TakeDamage(10f);
        } 
    }

    void ShootBullet() {
        if (CanShoot)
            StartCoroutine(Shoot());
    }

    IEnumerator Shoot() {
        CanShoot = false;
        enemyBulletCopy = Instantiate(enemyBullet, firePoint);
        yield return new WaitForSeconds(0.3f);
        CanShoot = true;
    }
}
