using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TwoBasicEnemies : SimpleEnemyPattern
{
    int deadEnemyCount;

    private List<Coroutine> coroutines;
    // Start is called before the first frame update
    void Start()
    {
        deadEnemyCount = 0;
        EnemyDeathEventHandler.instance.onEnemyDeath += OnEnemyDisable;

        currentEnemies = new List<GameObject>
        {
            null,
            null
        };

        coroutines = new List<Coroutine>
        {
            null,
            null
        };

        for (int i = 0; i < currentEnemies.Count; i++) {
            int r = UnityEngine.Random.Range(0,enemyPool.Count);
            currentEnemies[i] = Instantiate(enemyPool[r]);
            currentEnemies[i].SetActive(false);
        }

        int lastPosition = 5;

        for (int i = 0; i < currentEnemies.Count; i++) {
            int l = UnityEngine.Random.Range(0,lanes.Count);
            if (l == lastPosition) {
                int r = UnityEngine.Random.Range(1,lanes.Count);
                l = (l+r) % lanes.Count;
            }
            currentEnemies[i].transform.position = lanes[l];
            lastPosition = l;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
            StartCoroutine(StartSequence());
    }

    IEnumerator StartSequence() {
        start = false;
        foreach(GameObject e in currentEnemies)
            e.SetActive(true);
        //Sequentially "Spawn" enemies at 0.75-sec intervals into the field and activate them when they are midscreen
        for (int i = 0; i < currentEnemies.Count; i++) {
            coroutines[i] = StartCoroutine(EnemyMoveRoutine(currentEnemies[i]));
            //Debug.Log("spawning enemy");
            yield return new WaitForSeconds(0.75f);
        }

        yield return new WaitForSeconds(0f);
        //Sequence complete (enemies are either all offscreen or dead)
    }

    private void OnEnemyDisable(GameObject g)
    {
        for (int i = 0; i < currentEnemies.Count; i++) {
            if (GameObject.ReferenceEquals(g, currentEnemies[i])) {
                deadEnemyCount++;
                //Destroy(gameObject);
            }
        }

        if (deadEnemyCount == currentEnemies.Count) {
            EnemyDeathEventHandler.instance.onEnemyDeath -= OnEnemyDisable;
            for (int i = 0; i < currentEnemies.Count; i++) {
                StopCoroutine(coroutines[i]);
                Destroy(currentEnemies[i]);
            }
            StopAllCoroutines();
            CancelAllCoroutines();
            isFinished = true;
            gameObject.SetActive(false);
            AnnounceCompletion();
        }
    }
}
