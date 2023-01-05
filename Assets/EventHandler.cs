using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public static EventHandler instance;

    void Awake()
    {
        instance = this;
    }

    public event Action<int> onEnemyDeath;

    public void EnemyDeathTrigger(int a) {
        if (onEnemyDeath != null) {
            onEnemyDeath(a);
        }
    }
}
