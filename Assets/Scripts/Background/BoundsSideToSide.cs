using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsSideToSide : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {
        //Debug.Log("amongus");
        if (col.gameObject.tag == "Enemies") {
            //Debug.Log("breakfast");
            if (col.transform.parent.tag != "Boss")
                col.transform.parent.GetComponent<EnemyAI>().SideToSideSwap();
        } 
    }
}
