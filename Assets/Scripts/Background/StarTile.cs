using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarTile : MonoBehaviour
{
    public List<GameObject> pool;
    public float speed = 0;
    GameObject g;

    // Start is called before the first frame update
    void Start()
    {
        int r = Random.Range(0,pool.Count);
        g = Instantiate(pool[r],transform);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= Vector3.down * Time.deltaTime * speed;
    }
}
