using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEnemyAI : EnemyAI
{

    public float ShootSpeed = 2f;
    public float ShootWait = 0.25f;
    public int numBullets = 3;
    private GameObject b;

    private bool canShoot = true;

    void Awake() {
        SetEnemyType(3);
    }

    public override void Shoot() {
        if (canShoot&&StartAI) StartCoroutine(ShootRoutine());
    }

    IEnumerator ShootRoutine() {
        canShoot = false;
        Vector2 fp = transform.position;
        //Vector2 player = PlayerScript.instance.transform.position;
        Vector2 direction;

        for (int i = 0; i < numBullets; i++) {
            b = BulletPoolScript.instance.GetBullet();
            b.transform.position = fp;

            direction = new Vector2(-1, 0);

            direction = Quaternion.Euler(0, 0, (180/(numBullets-1))*i)*direction;

            b.GetComponent<BulletMovement>().SetSpeed(ShootSpeed);
            b.GetComponent<BulletMovement>().SetDirection(direction.x, direction.y);
            b.SetActive(true);
        }

        yield return new WaitForSeconds(ShootWait);
        canShoot = true;
    }

    public override void Stop() {
        StopCoroutine(ShootRoutine());
    }
}
