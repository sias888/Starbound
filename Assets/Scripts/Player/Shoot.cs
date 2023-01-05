using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    enum ShootState {
        Shoot,
        NoShoot
    }


    public Transform shootPoint;
    public GameObject bulletPrefab;
    private ShootState State;

    public int SHOOTCOOLDOWN = 10;
    private float ENERGYMAX = 100;
    public float ENERGYCOST = 5;
    public float ENERGYFILL = 0;

    private int shootCount;
    private float energyCurrent;

    void Start() {
        State = ShootState.Shoot;
        shootCount = 0;
        energyCurrent = ENERGYMAX;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && State == ShootState.Shoot) {
            Debug.Log(energyCurrent);

            if (energyCurrent >= ENERGYCOST) {
                ShootBullet();
                energyCurrent -= ENERGYCOST;
                if (energyCurrent < -10) {
                    energyCurrent = -10;
                }
                State = ShootState.NoShoot;
            }

        } else if (State == ShootState.Shoot && !Input.GetKey(KeyCode.Space)) {
            Debug.Log(energyCurrent);


            if (energyCurrent < ENERGYMAX) {
                energyCurrent += ENERGYFILL;
            }

            if (energyCurrent > ENERGYMAX) {
                energyCurrent = ENERGYMAX;
            }
        }

        if (shootCount >= SHOOTCOOLDOWN) {
            shootCount = 0;
            State = ShootState.Shoot;
        } else {
            shootCount++;
        }

        //Mathf.Clamp(energyCurrent, 0, ENERGYMAX);

    }

    private void ShootBullet() {
        //Spawn a bullet prefab. Give it a velocity.
        //Debug.Log("Shots fired!");

        Instantiate(bulletPrefab, shootPoint);
    }
}
