using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int EnemyType;
    public void SetEnemyType(int i) {
        EnemyType = i;
    }

    public bool StartAI;

    public void TriggerAI(bool b) {
        StartAI = b;
    }

    void Update() {
        if (StartAI) {
            Shoot();
        } else {
            Stop();
        }
    }

    public void Move(Vector2 target, float movetime) {
        Vector2 cur = transform.position;
        Vector2 dir = target - cur;
        StartCoroutine(MoveToLocation(dir, movetime));
    }

    IEnumerator MoveToLocation(Vector2 d, float movetime) {
        Vector3 dir = d;

        for (int i = 0; i < 100; i++) {
            transform.position = transform.position + dir/100;
            yield return new WaitForSeconds(movetime/100);
        }
    }

    public virtual void Shoot() {}

    public virtual void Stop() {}

    public virtual void OnDeath() {
        StartAI = false;
        transform.gameObject.SetActive(false);
        EventHandler.instance.EnemyDeathTrigger(EnemyType);
        Debug.Log("dying!!! " + EnemyType);
    }

}
