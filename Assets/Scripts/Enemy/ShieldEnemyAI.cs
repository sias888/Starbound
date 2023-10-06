using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemyAI : EnemyAI
{
    Transform shield;
    Transform hurtbox;

    //public bool TurnOnAI;
    public float ShootTime;
    public float bulletSpeed = 4f;
    public GameObject bullet;
    public GameObject firePoint;

    private GameObject b;
    private bool canShoot = true;

    // Start is called before the first frame update
    void Awake()
    {
        shield = transform.parent.GetChild(1);
        hurtbox = transform.GetChild(1);

        StartAI = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (shield.gameObject.activeInHierarchy) {
            hurtbox.gameObject.SetActive(false);
        } else {
            hurtbox.gameObject.SetActive(true);
        }

        if (StartAI && canShoot) {

            //StartCoroutine(ShootCoroutine());

        }
    }

    IEnumerator ShootCoroutine() {
        canShoot = false;
        b = Instantiate(bullet);
        b.transform.position = firePoint.transform.position + new Vector3(0.35f,0,0);
        b.GetComponent<BulletMovement>().SetDirection(transform.up*-1);
        b.GetComponent<BulletMovement>().SetSpeed(bulletSpeed);
        Destroy(b,3f);

        b = Instantiate(bullet);
        b.transform.position = firePoint.transform.position + new Vector3(-0.35f,0,0);
        b.GetComponent<BulletMovement>().SetDirection(transform.up*-1);
        b.GetComponent<BulletMovement>().SetSpeed(bulletSpeed);
        Destroy(b,3f);


        yield return new WaitForSeconds(ShootTime);
        canShoot = true;
    }

    
}
