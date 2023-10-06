using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuckshotAI : EnemyAI
{
    // Start is called before the first frame update public float ShootTime;
    public GameObject bullet;
    public GameObject firePoint;

    public float ShootTime;
    public float bulletSpeed;
    public int numBullets;
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


        for (int i = 0-2; i<numBullets-2; i++) {
                b = Instantiate(bullet);

                b.transform.position = firePoint.transform.position;

                //Set bullet speed
                b.GetComponent<BulletMovement>().SetSpeed(bulletSpeed);

                //Rotate bullet.transform.pos to correct angle
                Vector2 dir = -1*transform.up;
                dir = Quaternion.Euler(0, 0, (90/numBullets)*i)*dir;
                b.transform.position = transform.position + new Vector3(dir.x, dir.y, 0);
                b.GetComponent<BulletMovement>().SetDirection(dir);

                Destroy(b,3f);
        }


        yield return new WaitForSeconds(ShootTime);
        canShoot = true;
    }

}
