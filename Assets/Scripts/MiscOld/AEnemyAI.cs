using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEnemyAI : EnemyAI
{

    //private bool IsActive;

    public float ShootSpeed = 2f;
    public float ShootWait = 0.25f;
    private GameObject b;

    private bool canShoot = true;

    void Awake() {
        //SetEnemyType(0);
    }

    /*void SetIsActive(bool b) {
        IsActive = b;
    }

    void Update() {
        if (IsActive) {
            StartCoroutine(Shoot());
        }
    }
    */

    public override void Shoot() {
        if (canShoot&&StartAI) StartCoroutine(ShootRoutine());
    }

    IEnumerator ShootRoutine() {
        canShoot = false;
        Vector2 fp = transform.position;
        Vector2 player = PlayerScript.instance.transform.position;

        b = BulletPoolScript.instance.GetBullet();
        b.transform.position = fp;
        b.GetComponent<BulletMovement>().SetSpeed(ShootSpeed);
        b.GetComponent<BulletMovement>().SetDirection(player.x - fp.x, player.y - fp.y);
        b.SetActive(true);

        yield return new WaitForSeconds(ShootWait);
        canShoot = true;
    }

    public override void Stop() {
        StopCoroutine(ShootRoutine());
    }
}
