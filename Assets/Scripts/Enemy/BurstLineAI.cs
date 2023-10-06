using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstLineAI : EnemyAI
{
     // Start is called before the first frame update public float ShootTime;
    public GameObject bullet;
    public GameObject firePoint;

    public float BurstShootTime;
    public int BurstNum = 3;
    public float ShootTime;

    public float bulletSpeed;
    private GameObject b;
    private bool canShoot = true;

    // Start is called before the first frame update
    void Awake()
    {
        StartAI = false;
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

        for (int i = 0; i < BurstNum; i++) {

        b = Instantiate(bullet);
        b.transform.position = firePoint.transform.position + 0.6f*transform.right;
        b.GetComponent<BulletMovement>().SetDirection(transform.up*-1);
        b.GetComponent<BulletMovement>().SetSpeed(bulletSpeed);
        Destroy(b,3f);

        b = Instantiate(bullet);
        b.transform.position = firePoint.transform.position + -0.6f*transform.right;
        b.GetComponent<BulletMovement>().SetDirection(transform.up*-1);
        b.GetComponent<BulletMovement>().SetSpeed(bulletSpeed);
        Destroy(b,3f);

        yield return new WaitForSeconds(BurstShootTime);

        }

        yield return new WaitForSeconds(ShootTime);
        canShoot = true;
    }
}
