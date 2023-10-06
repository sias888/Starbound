using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayAI : EnemyAI
{
    // Start is called before the first frame update public float ShootTime;
    public GameObject bullet;
    public GameObject firePoint;

    public float ShootTime;
    public float timeBetweenBullets;
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
            StartCoroutine(ShootCoroutineFlipped());
        }
    }

    IEnumerator ShootCoroutine() {
        canShoot = false;


        for (int i = 0; i<numBullets; i++) {
                b = Instantiate(bullet);

                b.transform.position = firePoint.transform.position + -0.6f*transform.right;

                //Set bullet speed
                b.GetComponent<BulletMovement>().SetSpeed(bulletSpeed);

                //Rotate bullet.transform.pos to correct angle
                Vector2 dir = -1*transform.up;
                dir = Quaternion.Euler(0, 0, (120/numBullets)*i)*dir;
                //offset by -60 to start.
                dir = Quaternion.Euler(0, 0, -120)*dir;
                //b.transform.position = transform.position + new Vector3(dir.x, dir.y, 0);
                b.GetComponent<BulletMovement>().SetDirection(dir);

                Destroy(b,3f);
                yield return new WaitForSeconds(timeBetweenBullets);
        }

        for (int i = 0; i<numBullets; i++) {
                b = Instantiate(bullet);

                b.transform.position = firePoint.transform.position + -0.6f*transform.right;

                //Set bullet speed
                b.GetComponent<BulletMovement>().SetSpeed(bulletSpeed);

                //Rotate bullet.transform.pos to correct angle
                Vector2 dir = -1*transform.up;
                dir = Quaternion.Euler(0, 0, (-120/numBullets)*i)*dir;
                //offset by -60 to start.
                //dir = Quaternion.Euler(0, 0, 90)*dir;
                //b.transform.position = transform.position + new Vector3(dir.x, dir.y, 0);
                b.GetComponent<BulletMovement>().SetDirection(dir);

                Destroy(b,3f);
                yield return new WaitForSeconds(timeBetweenBullets);
        }


        yield return new WaitForSeconds(ShootTime);
        canShoot = true;
    }

    IEnumerator ShootCoroutineFlipped() {
        canShoot = false;


        for (int i = 0; i<numBullets; i++) {
                b = Instantiate(bullet);

                b.transform.position = firePoint.transform.position + 0.6f*transform.right;

                //Set bullet speed
                b.GetComponent<BulletMovement>().SetSpeed(bulletSpeed);

                //Rotate bullet.transform.pos to correct angle
                Vector2 dir = -1*transform.up;
                dir = Quaternion.Euler(0, 0, (-120/numBullets)*i)*dir;
                //offset by -60 to start.
                dir = Quaternion.Euler(0, 0, 120)*dir;
                //b.transform.position = transform.position + new Vector3(dir.x, dir.y, 0);
                b.GetComponent<BulletMovement>().SetDirection(dir);

                Destroy(b,3f);
                yield return new WaitForSeconds(timeBetweenBullets);
        }

        for (int i = 0; i<numBullets; i++) {
                b = Instantiate(bullet);

                b.transform.position = firePoint.transform.position + 0.6f*transform.right;

                //Set bullet speed
                b.GetComponent<BulletMovement>().SetSpeed(bulletSpeed);

                //Rotate bullet.transform.pos to correct angle
                Vector2 dir = -1*transform.up;
                dir = Quaternion.Euler(0, 0, (120/numBullets)*i)*dir;
                //offset by -60 to start.
                //dir = Quaternion.Euler(0, 0, -90)*dir;
                //b.transform.position = transform.position + new Vector3(dir.x, dir.y, 0);
                b.GetComponent<BulletMovement>().SetDirection(dir);

                Destroy(b,3f);
                yield return new WaitForSeconds(timeBetweenBullets);
        }


        yield return new WaitForSeconds(ShootTime);
        canShoot = true;
    }

}
