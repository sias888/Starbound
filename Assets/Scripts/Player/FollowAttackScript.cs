using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAttackScript : MonoBehaviour
{
    //private Attack _attack;
    List<GameObject> enemiesAlreadyHit = new List<GameObject>();

    // Start is called before the first frame update
    void Awake() {
        //_attack = GameObject.FindGameObjectWithTag("Player").GetComponent<Attack>();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Enemies") {
            if (!enemiesAlreadyHit.Contains(col.gameObject)) {
                col.gameObject.GetComponentInParent<Enemy>().TakeDamage(Attack.instance.AttackDmgVal * ScoreManager.instance.GetScaling());
                Attack.instance.FillStam();
                ScoreManager.instance.Increment(100);
                enemiesAlreadyHit.Add(col.gameObject.transform.parent.gameObject);
            }
        }

        /*if (col.gameObject.tag == "Enemy Projectile") {
            col.gameObject.GetComponent<EnemyBulletHitbox>().Deflect();
        }*/
    }
}
