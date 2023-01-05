using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

    enum ShootState {
        CanShoot,
        NoShoot
    }

    public Transform shootPoint;
    public GameObject bulletPrefab;
    private GameObject bulletPrefabCopy;
    private ShootState shootState;
    private bool canFill;

    float shootCost;

    //private WaitForSeconds shootCoolDown = new WaitForSeconds(1);
    public float SHOOTCOOLDOWN = 0.07f;
    public float FILLCOOLDOWN = 0.025f;
    public float FILLVALUE = 1.3f;


    // Start is called before the first frame update
    void Start()
    {
        shootState = ShootState.CanShoot;
        canFill = true;
    }

    // Update is called once per frame
    void Update()
    {
        float shoot = Input.GetAxis ("Shoot");
        if ((Input.GetKey(KeyCode.LeftShift) || !(shoot == 0f)) && shootState == ShootState.CanShoot /*&& EnergyBar.instance.canFire(shootCost)*/) {
            Shoot();
        } else if (!Input.GetKey(KeyCode.LeftShift) && canFill) {
            //Fill(FILLVALUE);
        }
    }

    void Shoot() {
        //EnergyBar.instance.useEnergy(shootCost);
        bulletPrefabCopy = Instantiate(bulletPrefab, shootPoint);
        shootState = ShootState.NoShoot;
        StartCoroutine(ShootCooldown());
    }

    void Fill(float FILLVALUE) {
        EnergyBar.instance.fillEnergy(FILLVALUE);
        canFill = false;
        StartCoroutine(FillCooldown());
    }

    IEnumerator ShootCooldown() {
        yield return new WaitForSeconds(SHOOTCOOLDOWN);
        shootState = ShootState.CanShoot;
    }

    IEnumerator FillCooldown() {
        yield return new WaitForSeconds(FILLCOOLDOWN);
        canFill = true;
    }
}
