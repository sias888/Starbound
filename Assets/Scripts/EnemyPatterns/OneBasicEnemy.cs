using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneBasicEnemy : SimpleEnemyPattern
{
    // Start is called before the first frame update
    private GameObject enemy;

    Coroutine emr;
    void Start()
    {
        EnemyDeathEventHandler.instance.onEnemyDeath += OnEnemyDisable;

        int r = Random.Range(0,enemyPool.Count);
        enemy = Instantiate(enemyPool[r]);

        enemy.SetActive(false);

        int l = Random.Range(0,lanes.Count);
        enemy.transform.position = lanes[l];
    }

    // Update is called once per frame
    void Update()
    {
        if (start) {
            enemy.SetActive(true);
            emr = StartCoroutine(EnemyMoveRoutine(enemy));
            start = false;
        }
    }

    private void OnEnemyDisable(GameObject g)
    {
        if (GameObject.ReferenceEquals(g, enemy)) {
            EnemyDeathEventHandler.instance.onEnemyDeath -= OnEnemyDisable;
            StopCoroutine(emr);
            Destroy(enemy);
            CancelAllCoroutines();
            isFinished = true;
            gameObject.SetActive(false);
            AnnounceCompletion();
            //Destroy(gameObject);
        }
    }


}
