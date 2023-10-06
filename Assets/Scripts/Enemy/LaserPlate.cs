using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPlate : MonoBehaviour
{

    public bool spin = false;
    public float speed = 20f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (spin) {
            transform.Rotate(0, 0, speed*Time.deltaTime);
        }
    }
}
