using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DustTile : MonoBehaviour
{
    public List<GameObject> pool;
    public float speed = 0;
    GameObject dust;

    // Start is called before the first frame update
    void Start()
    {
        int r = Random.Range(0,pool.Count);
        dust = Instantiate(pool[r],transform);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= Vector3.down * Time.deltaTime * speed;
    }
}
