using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathEventHandler : MonoBehaviour
{
    public static EnemyDeathEventHandler instance;

    void Awake()
    {
        instance = this;
    }

    public event Action<GameObject> onEnemyDeath;

    public void EnemyDeathTrigger(GameObject g) {
        if (onEnemyDeath != null) {
            onEnemyDeath(g);
        }
    }
}
