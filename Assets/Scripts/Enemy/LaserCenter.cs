using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCenter : EnemyAI
{
    public override void OnDeath()
    {
        GetComponentInParent<LaserMiniboss>().OnDeath();
        base.OnDeath();
    }
}
