using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEditor;
using UnityEngine;

public class DustController : MonoBehaviour
{
    public GameObject DustTile;

    GameObject currentTile;
    GameObject nextTile;
    GameObject oldTile;

    [SerializeField] float speed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        currentTile = Instantiate(DustTile);
        currentTile.GetComponent<BackgroundTileComponent>().speed = -speed;

        //SpawnNewTile();
    }

    //bool canSpawnNext = true;

    // Update is called once per frame
    void Update()
    {
        if (currentTile.transform.position.y <= 0) { 
            Destroy(oldTile);
            SpawnNewTile();
            oldTile = currentTile;
            currentTile = nextTile;
            nextTile = null;
        }
    }

    private void SpawnNewTile() {
        nextTile = Instantiate(DustTile,Vector3.up * 10 + currentTile.transform.position, Quaternion.identity);
        nextTile.GetComponent<BackgroundTileComponent>().speed = -speed;
    }
}
