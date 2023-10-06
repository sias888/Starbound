using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyAI : EnemyAI
{

    private GameObject b;
    //public GameObject beam;
    public GameObject fp;
    public float ShootWait = 5f;
    public float ShootDuration = 5f;

    private bool canShoot = true;

    void Awake() {
        //SetEnemyType(2);
    }

    public override void Shoot() {
        if (canShoot&&StartAI) StartCoroutine(ShootRoutine());
    }

    IEnumerator ShootRoutine() {
        canShoot = false;

        /*b = Instantiate(beam, fp.transform);
        b.SetActive(false);
        Destroy(b, ShootDuration*1.2f);
        b.SetActive(true);
        */

        //request beam from pool
        b = BulletPoolScript.instance.GetBeam(fp.transform);
        //b.SetActive(true);

        //since nothing needs to be done, set to active
        //b.transform.position = fp.transform.position;
        //b.transform.SetParent(fp.transform, false);
        b.SetActive(true);

        yield return new WaitForSeconds(ShootDuration);

        //set inactive after given duration
        //b.transform.SetParent(null, false);
        //b.SetActive(false);
        b.GetComponent<BossBeamHitbox>().anim.SetTrigger("Finish");

        yield return new WaitForSeconds(ShootWait);
        canShoot = true;
    }

    public override void Stop() {
        if (b) {
            b.transform.SetParent(null, false);
            b.SetActive(false);
        }
        StopCoroutine(ShootRoutine());
    }

    public override void OnDeath() {
        StopCoroutine(ShootRoutine());

        if (b) {
            b.transform.SetParent(null, false);
            b.SetActive(false);
        }

        base.OnDeath();
    }


}
