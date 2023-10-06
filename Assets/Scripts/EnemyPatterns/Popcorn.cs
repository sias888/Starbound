using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popcorn : EnemyAI
{
    private void Awake() {
        gameObject.GetComponent<EnemyHP>().ArmorScaling = 0f;
    }
    public override void TriggerAI(bool b)
    {
        StartAI = b;
        if (b == true) {
            gameObject.GetComponent<EnemyHP>().Armor = true;
        }
    }

    public void ResetScaling() {
        gameObject.GetComponent<EnemyHP>().Armor = false;
    }
}
