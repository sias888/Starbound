using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyPattern : MonoBehaviour
{
    //public static SimpleEnemyPattern instance;
    public List<GameObject> currentEnemies;
    public List<GameObject> enemyPool;

    public bool start = false;

    protected List<Vector3> lanes;

    private void Awake() {
        //instance = this;
        lanes = new List<Vector3>();
        for (int i = 0; i < 3; i++) {
            lanes.Add(new Vector3());
            lanes[i] = new Vector3(-2f + i*2, 5.5f, 0);
        }
    }

    public bool isFinished = false;

    public IEnumerator EnemyMoveRoutine(GameObject enemy) {
        //move enemy to center screen at 5units/second, StartAI when reached, and wait
        enemy.GetComponent<EnemyAI>().TriggerAI(false);
        float dist = Random.Range(-3.5f-1,-3.5f+2);
        enemy.GetComponent<EnemyAI>().MoveAndStart(new Vector2(0,dist), 0.75f);
        yield return new WaitForSeconds(3f+3f);

        //turn off AI and move down to offscreen position
        enemy.GetComponent<EnemyAI>().TriggerAI(false);
        enemy.GetComponent<EnemyAI>().Move(new Vector2(0,-10), 2f);
        yield return new WaitForSeconds(2.5f);

        if (enemy.activeInHierarchy)
            enemy.GetComponent<EnemyAI>().OnDeath();
    }

    public IEnumerator MoveToMiddle(GameObject enemy) {
        //move enemy to center screen at 5units/second, StartAI when reached, and wait
        enemy.GetComponent<EnemyAI>().TriggerAI(false);
        float dist = Random.Range(-3.5f-1,-3.5f+2);
        enemy.GetComponent<EnemyAI>().MoveAndStart(new Vector2(0,dist), 0.75f);
        yield return new WaitForSeconds(3f+3f);
    }

    public IEnumerator SideToSide(GameObject enemy) {
        //move enemy to center screen at 5units/second, StartAI when reached, move side to side repeatedly, and wait
        enemy.GetComponent<EnemyAI>().TriggerAI(false);
        float dist = Random.Range(-3.5f-1,-3.5f+2);
        yield return StartCoroutine(enemy.GetComponent<EnemyAI>().MoveToLocationAndStart(new Vector2(0,dist), 0.75f));
        enemy.GetComponent<EnemyAI>().SideToSideTrigger(true,4f);
        
        yield return new WaitForSeconds(3f+3f);

        //turn off AI and move down to offscreen position
        enemy.GetComponent<EnemyAI>().TriggerAI(false);
        enemy.GetComponent<EnemyAI>().SideToSideTrigger(false,0f);
        enemy.GetComponent<EnemyAI>().Move(new Vector2(0,-10), 2f);
        yield return new WaitForSeconds(2.5f);

        if (enemy.activeInHierarchy)
            enemy.GetComponent<EnemyAI>().OnDeath();
    }

    public void CancelAllCoroutines() {
        StopAllCoroutines();
    }

    protected void AnnounceCompletion() {
        start = false;
        StopAllCoroutines();
        gameObject.SetActive(false);
        PatternCompletionEventHandler.instance.PatternCompleteTrigger(this.gameObject);
    }

}
