using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public float speed = 0f;

    public GameObject IMG;

    private GameObject CurrentImg;
    private GameObject NextImg;

    private float ImgSize = 60f;
    private int count = 1;
    //private int RstepSize = 60;

    // Start is called before the first frame update
    void Start()
    {
        CurrentImg = transform.GetChild(0).gameObject;
        NextImg = Instantiate(IMG, transform);
        NextImg.transform.position = new Vector3(0,ImgSize,0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Move down at speed rate
        transform.position = transform.position - new Vector3(0, speed*Time.fixedDeltaTime, 0);
        gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).transform.position = Vector3.zero;

        if (transform.position.y + ImgSize*count <= 0.5 && transform.position.y + ImgSize*count >= -0.5) {
            //Debug.Log("hi");
            count++;
            GameObject TempImg = Instantiate(IMG, transform);
            TempImg.transform.position = new Vector3(0, NextImg.transform.position.y + ImgSize,0);
            Destroy(CurrentImg);
            CurrentImg = NextImg;
            NextImg = TempImg;
            TempImg = null;
        }
    }
}
