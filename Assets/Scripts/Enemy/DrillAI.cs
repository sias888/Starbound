using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class DrillAI : EnemyAI
{
    //public float acceleration = 10f;
    //[SerializeField]
    public float iSpeed = 3f;
    //Vector2 direction = Vector2.up;

    //float vX = 0;
    //float vY = 0;

    bool startDirectionChange = true;
    bool exponential = false;
    //[SerializeField]
    float aVelocity = 360;
    // Start is called before the first frame update
    void Start()
    {
        TriggerAI(true);
        StartCoroutine(DirectionChange());
        explosionModifier = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        //using kinematic equations, move enemy
        Vector3 playerPos = PlayerScript.instance.transform.position;
        //float t = Time.deltaTime;

        /*
        vX = direction.x*iSpeed;
        vY = direction.y*iSpeed;

        Vector3 targetPos = gameObject.transform.position;
        targetPos.x += vX*t;
        targetPos.y += vY*t;
        gameObject.transform.position = targetPos;
        */

        if(startDirectionChange) {
            Vector3 direction = playerPos - transform.position;
            direction.Normalize();
            float rotDirection = Vector3.Cross(direction,transform.up).z;
            transform.GetComponent<Rigidbody2D>().angularVelocity = (aVelocity)*(-rotDirection);
        } else {
            transform.GetComponent<Rigidbody2D>().angularVelocity = 0;
        }

        transform.GetComponent<Rigidbody2D>().velocity = transform.up * iSpeed;
        if (exponential)
            iSpeed *= 1.0008f;
            
    }

    IEnumerator DirectionChange() {
        startDirectionChange = false;
        //iSpeed *= 0.5f;
        yield return new WaitForSeconds(0.5f);

        startDirectionChange = true;
        exponential = true;

        yield return new WaitForSeconds(3f);

        startDirectionChange = false;

        yield return new WaitForSeconds(2f);

        OnDeath();
    }
}
