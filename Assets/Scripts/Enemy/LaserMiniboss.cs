using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LaserMiniboss : EnemyAI
{

    public bool b = false;//For debugging purposes--only to be used in the editor
    private void Update() {
        if (b) TriggerAI(b);

        b=false;
    }

    public override void TriggerAI(bool b)
    {
        EnemyAI[] enemies = GetComponentsInChildren<EnemyAI>(false);
        for (int i = 1; i < enemies.Count<EnemyAI>(); i++) enemies[i].TriggerAI(b);

        GetComponentInChildren<LaserPlate>().spin = b;
    }

    //Kills all the arms
    public override void OnDeath()
    {
        LaserArm[] arms = GetComponentsInChildren<LaserArm>(false);
        foreach(LaserArm arm in arms) arm.OnDeath();
    }

    public void SetSpinSpeed(float f) {
        GetComponentInChildren<LaserPlate>().speed = f;
    }

}
