using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserArm : EnemyAI
{

    public GameObject beam;
    public GameObject firepoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject l;
    void ShootLaser() {
        l = Instantiate(beam, firepoint.transform);
        //BeamStartAudio.instance.PlayClip();
    }

    void FinishLaser() {
        l.GetComponent<Animator>().Play("ThickLaserFinishALT");
    }

    public override void TriggerAI(bool b)
    {
        if (b == true) {
            ShootLaser();
        }

        if (b == false) {
            FinishLaser();
        }

        base.TriggerAI(b);
    }

    public override void OnDeath()
    {
        if (l) FinishLaser();
        base.OnDeath();
    }
}
