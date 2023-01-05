using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{

    public float SPEED = 0.75f;
    private Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
        Vector2 velocity = new Vector2(0, -SPEED);
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = velocity;
    }

    public void Deflect() {
        Vector2 velocity = new Vector2(0, SPEED*3);
        rigidBody.velocity = velocity;
        transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
    }
}
