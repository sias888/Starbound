using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAI : EnemyAI
{

    public GameObject laser;

    private List<GameObject> lasers;

    public List<Transform> firepoints;

    public float ShootDuration;

    public float ShootWait;

    private bool canShoot = true;

    // Start is called before the first frame update
    void Awake()
    {
        StartAI = false;
        lasers = new List<GameObject>();

        lasers.Add(null);
        lasers.Add(null);
        lasers.Add(null);
        lasers.Add(null);
    }

    // Update is called once per frame
    void Update()
    {
        if (StartAI && canShoot) {
            StartCoroutine(ShootCoroutine());
        }
    }

    IEnumerator ShootCoroutine() {
        canShoot = false;

        for (int i = 0; i<4; i++) {
            lasers[i] = Instantiate(laser, firepoints[i]);
        }

        yield return new WaitForSeconds(ShootDuration);

        foreach (GameObject beam in lasers) {
            if (beam) 
                beam.GetComponent<Animator>().Play("ThickLaserFinishALT");
        }

        yield return new WaitForSeconds(ShootWait);
        canShoot = true;
    }

    public override void OnDeath() {
        StartAI = false;
        foreach (GameObject beam in lasers) {
            if (beam) 
                Destroy(beam);
        }
        transform.gameObject.SetActive(false);
        EnemyDeathEventHandler.instance.EnemyDeathTrigger(this.gameObject);
        //Debug.Log("dying!!! " + EnemyType);
    }
}
