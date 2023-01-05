using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{

    Rigidbody2D myRigidBody;
    Animator animator;

    List<GameObject> enemiesAlreadyHit = new List<GameObject>();

    public float SPEED = 10f;

    // Start is called before the first frame update
    void Start()
    {
        //Vector2 velocity = new Vector2(0, SPEED);
        myRigidBody = gameObject.GetComponent<Rigidbody2D>();
        //myRigidBody.velocity = velocity;
        animator = gameObject.GetComponent<Animator>();
    }

    void FixedUpdate() {
        Vector3 velocity = new Vector3(0, SPEED, 0);
        transform.position += velocity * Time.fixedDeltaTime;
    }


    void OnTriggerEnter2D(Collider2D col) {
        //Debug.Log("Triggered!");
        if (col.gameObject.tag == "Enemies") {
            if (!enemiesAlreadyHit.Contains(col.gameObject)) {
                col.gameObject.GetComponentInParent<Enemy>().TakeDamage(3f);
                enemiesAlreadyHit.Add(col.gameObject);
                myRigidBody.velocity = Vector2.zero;
                animator.Play("BulletExplode");
                Destroy(gameObject, 0.1f);
            }
        }

        if (col.gameObject.tag == "UpperBound") {
            Destroy(gameObject, 0.1f);
        }
    }

}
