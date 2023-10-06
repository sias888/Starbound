using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolScript : MonoBehaviour
{
    public static BulletPoolScript instance;

    private List<GameObject> bullets;
    private List<GameObject> beams;
    private List<GameObject> thickBeams;

    public GameObject bullet;
    public GameObject beam;
    public GameObject thickBeam;

    void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        bullets = new List<GameObject>();
        beams = new List<GameObject>();
    }

    //Returns an inactive bullet
    public GameObject GetBullet() {

        //Cycle until find inactive bullet
        if (bullets.Count > 0) {
            for (int i = 0; i < bullets.Count; i++) {
                if (!bullets[i].activeInHierarchy && !bullets[i].GetComponent<BulletMovement>().destroyed) {
                    return bullets[i];
                }
            }

        }

        //Since no inactive bullet was returned, create new bullet and return it
        GameObject bul  = Instantiate(bullet);
        bul.SetActive(false);
        bullets.Add(bul);
        return bul;

    }

    //Returns an inactive beam;
    public GameObject GetBeam(Transform parent) {

        if (beams.Count > 0) {
            for (int i = 0; i < beams.Count; i++) {
                if (!beams[i].activeInHierarchy) { // cycle until find inactive beam
                    beams[i].transform.SetParent(parent, false);
                    return beams[i];
                }
            }
        }

        //No inactive beam was returned, therefore create one and return
        GameObject b = Instantiate(beam);
        b.SetActive(false);
        beams.Add(b);
        b.transform.SetParent(parent, false);
        return b;
    }

    //Returns an inactive Thick beam;
    public GameObject GetThickBeam() {

        if (thickBeams.Count > 0) {
            for (int i = 0; i < thickBeams.Count; i++) {
                if (!thickBeams[i].activeInHierarchy) { // cycle until find inactive beam
                    return thickBeams[i];
                }
            }
        }

        //No inactive beam was returned, therefore create one and return
        GameObject tb = Instantiate(thickBeam);
        tb.SetActive(false);
        thickBeams.Add(tb);
        return tb;
    }
}
