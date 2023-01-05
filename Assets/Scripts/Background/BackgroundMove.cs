using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public float speed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = transform.position - new Vector3(0, speed*Time.fixedDeltaTime, 0);
        if (transform.position.y < -86.18f) {
            transform.position = new Vector3(0,-14.22f,0) - new Vector3(0, speed*Time.fixedDeltaTime, 0);
        } 
    }
}
