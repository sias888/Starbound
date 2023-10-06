using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternCompletionEventHandler : MonoBehaviour
{
    //public static EnemyDeathEventHandler instance;
    public static PatternCompletionEventHandler instance;

    void Awake()
    {
        instance = this;
    }

    //public event Action<GameObject> onEnemyDeath;
    public event Action<GameObject> onPatternCompletion;

    /*
    public void EnemyDeathTrigger(GameObject g) {
        if (onEnemyDeath != null) {
            onEnemyDeath(g);
        }
    }
    */

    public void PatternCompleteTrigger(GameObject g) {
        if (onPatternCompletion != null) {
            onPatternCompletion(g);
        }
    }
}
