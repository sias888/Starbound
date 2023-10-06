using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BackgroundTile : MonoBehaviour
{
    public List<GameObject> PlanetPool;
    public List<GameObject> StarPool;
    public List<GameObject> DustPool;

    GameObject planet;
    GameObject star;
    GameObject dust;
    // Start is called before the first frame update
    void Start()
    {
        int r = Random.Range(0,PlanetPool.Count);
        planet = Instantiate(PlanetPool[r], this.transform);

        r = Random.Range(0,StarPool.Count);
        star = Instantiate(StarPool[r], this.transform);

        r = Random.Range(0,DustPool.Count);
        dust = Instantiate(DustPool[r], this.transform);
    }

    private void Update() {
        
    }
}
