using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour
{
    Vector2 velocity;
    Rigidbody2D rigidBody;

    public float SPEED;

    public bool horizontal = true;

    //public Camera MainCamera;
    //Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        if (horizontal)
            velocity = new Vector2(SPEED, 0);
        else
            velocity = new Vector2(0, SPEED);
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        rigidBody.velocity = velocity;

        //screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;

        if (pos.x >= 4.72f ) {
            velocity.x = -SPEED;
            rigidBody.velocity = velocity;
            transform.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        } else if (pos.x <= -4.72) {
            velocity.x = SPEED;
            rigidBody.velocity = velocity;
            transform.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (pos.y >= 2.78) {
            velocity.y = -SPEED;
            rigidBody.velocity = velocity;
            transform.gameObject.GetComponent<SpriteRenderer>().flipY = true;
        }
        else if (pos.y <= -1.81) {
            velocity.y = SPEED;
            rigidBody.velocity = velocity;
            transform.gameObject.GetComponent<SpriteRenderer>().flipY = false;
        }
    }

}
