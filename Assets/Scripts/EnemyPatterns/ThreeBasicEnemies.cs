using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeBasicEnemies : SimpleEnemyPattern
{
    // Start is called before the first frame update
    void Start()
    {
        deadEnemyCount = 0;
        EnemyDeathEventHandler.instance.onEnemyDeath += OnEnemyDisable;

        currentEnemies = new List<GameObject>
        {
            null,
            null,
            null
        };

        for (int i = 0; i < 3; i++) {
            int r = UnityEngine.Random.Range(0,enemyPool.Count);
            currentEnemies[i] = Instantiate(enemyPool[r]);
            currentEnemies[i].SetActive(false);
        }

        int lastPosition = 5;

        for (int i = 0; i < 3; i++) {
            int l = Random.Range(0,lanes.Count);
            if (l == lastPosition) {
                int r = Random.Range(1,lanes.Count);
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
        for (int i = 0; i < 3; i++) {
            StartCoroutine(EnemyMoveRoutine(currentEnemies[i]));
            //Debug.Log("spawning enemy");
            yield return new WaitForSeconds(0.35f);
        }

        yield return new WaitForSeconds(0f);
        //Sequence complete (enemies are either all offscreen or dead)

    }

/*
    IEnumerator EnemyMoveRoutine(GameObject enemy) {
        //Get start x and y vals for enemy
        float x = enemy.transform.position.x;
        float y = enemy.transform.position.x;
        //move enemy to center screen at 5units/second and StartAI when reached
        float dist = Random.Range(-7f-2,-7f+2);
        if (enemy)
        enemy.GetComponent<EnemyAI>().MoveAndStart(new Vector2(0,dist), 2f);
        //Wait 3 seconds midscreen
        yield return new WaitForSeconds(3f+3f);
        if (enemy)
        enemy.GetComponent<EnemyAI>().TriggerAI(false);

        if (enemy)
        //Move down to offscreen position
        enemy.GetComponent<EnemyAI>().Move(new Vector2(0,-10), 3f);

        yield return new WaitForSeconds(4f);
        if (enemy)
        enemy.GetComponent<EnemyAI>().OnDeath();

    }
*/

    int deadEnemyCount = 0;
    private void OnEnemyDisable(GameObject g)
    {
        foreach (GameObject e in currentEnemies) {
            if (GameObject.ReferenceEquals(g,e)) deadEnemyCount++;
        }

        if (deadEnemyCount >= 3) {
            EnemyDeathEventHandler.instance.onEnemyDeath -= OnEnemyDisable;
            foreach(GameObject enemy in currentEnemies) {
                Destroy(enemy);
            }
            StopAllCoroutines();
            CancelAllCoroutines();
            //StopAllCoroutines();
            isFinished = true;
            gameObject.SetActive(false);
            AnnounceCompletion();
            //Destroy(gameObject);
        }
    }
}
