using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

    enum ShootState {
        CanShoot,
        NoShoot
    }

    public Transform[] shootPoints;
    //public Transform shootPoint2;
    //public Transform shootPoint3;
    //public Transform shootPoint4;
    //public GameObject bulletPrefab;
    //private GameObject bulletPrefabCopy;
    private ShootState shootState;
    //private bool canFill;

    //float shootCost;

    //private WaitForSeconds shootCoolDown = new WaitForSeconds(1);
    public float SHOOTCOOLDOWN = 0.07f;

    //[SerializeField]
    public float DAMAGE = 0.1f;
    //public float FILLCOOLDOWN = 0.025f;
    //public float FILLVALUE = 1.3f;

    public int MAXBULLETS = 100;
    // Start is called before the first frame update
    void Start()
    {
        shootState = ShootState.CanShoot;
        //canFill = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerScript.instance.isDead) return;

        PlayerBulletPool.instance.MAX_BULLETS = MAXBULLETS;
        if (PlayerInput.instance.getFirePressed() && shootState == ShootState.CanShoot 
        && !PauseControls.isPaused) {
            Shoot();
        }
    }


    /*
    void Shoot() {
        //EnergyBar.instance.useEnergy(shootCost);
        bulletPrefabCopy = Instantiate(bulletPrefab, shootPoint);
        bulletPrefabCopy.GetComponent<PlayerBulletScript>().SetDamage(DAMAGE);

        bulletPrefabCopy = Instantiate(bulletPrefab, shootPoint2);
        bulletPrefabCopy.GetComponent<PlayerBulletScript>().SetDamage(DAMAGE);

        bulletPrefabCopy = Instantiate(bulletPrefab, shootPoint3);
        bulletPrefabCopy.GetComponent<PlayerBulletScript>().SetDamage(DAMAGE);

        bulletPrefabCopy = Instantiate(bulletPrefab, shootPoint4);
        bulletPrefabCopy.GetComponent<PlayerBulletScript>().SetDamage(DAMAGE);
        
        shootState = ShootState.NoShoot;
        StartCoroutine(ShootCooldown());
    }
    */

    /*
    void Shoot() {
        List<GameObject> bullets = new List<GameObject>();

        for (int i = 0; i < 4; i++) {
            GameObject bullet = PlayerBulletPool.instance.GetBullet(shootPoints[i]);

            if (bullet == null) {
                foreach(GameObject b in bullets) {
                    b.SetActive(false);
                }
                return;
            }

            else {
                bullets.Add(bullet);
                bullet.GetComponent<PlayerBulletScript>().SetDamage(DAMAGE);
                bullets[i].SetActive(true);
            }
        }

        if (bullets.Count == 4) {
            for (int i = 0; i < 4; i++) {
                bullets[i].GetComponent<PlayerBulletScript>().SetDamage(DAMAGE);
                //bullets[i].transform.position = shootPoints[i].position;
            }
            shootState = ShootState.NoShoot;
            StartCoroutine(ShootCooldown());
        }

    }
    */

    void Shoot() {
        #nullable enable
        List<GameObject>? bullets = PlayerBulletPool.instance.GetBullets(shootPoints.Length);

        if (bullets == null) return;
        #nullable disable
        for(int i = 0; i < shootPoints.Length; i++) {
            bullets[i].transform.position = shootPoints[i].position;
            bullets[i].GetComponent<PlayerBulletScript>().SetDamage(DAMAGE);
            bullets[i].SetActive(true);
        }

        shootState = ShootState.NoShoot;
        StartCoroutine(ShootCooldown());
    }

    /*
    void Fill(float FILLVALUE) {
        EnergyBar.instance.fillEnergy(FILLVALUE);
        canFill = false;
        StartCoroutine(FillCooldown());
    }
    */

    IEnumerator ShootCooldown() {
        /*int poolCount = PlayerBulletPool.instance.poolCount();
        float bulletRateBonus = 0;

        //if current bullets are less than 25% of pool maximum, increase bullet fire rate up within range of 1.25 to 1.5.
        if (poolCount/MAXBULLETS <= 0.25f)
            bulletRateBonus = SHOOTCOOLDOWN*(0.5f-poolCount/MAXBULLETS);

        Debug.Log(SHOOTCOOLDOWN - bulletRateBonus);
        */
        yield return new WaitForSeconds(SHOOTCOOLDOWN);// - bulletRateBonus);
        shootState = ShootState.CanShoot;
    }

    /*
    IEnumerator FillCooldown() {
        yield return new WaitForSeconds(FILLCOOLDOWN);
        canFill = true;
    }
    */
}
