using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class PopcornGroup : EnemyAI
{
    // Start is called before the first frame update
    public AnimationCurve customCurveStart;
    public AnimationCurve customCurveEnd;

    public override void TriggerAI(bool b)
    {
        EnemyAI[] enemies = GetComponentsInChildren<EnemyAI>(false);
        for (int i = 1; i < enemies.Count<EnemyAI>(); i++) enemies[i].TriggerAI(b);

        if (b == true) {
            StartCoroutine(StartSequence());
        }
    }

    public Vector3 dir;

    private void Awake() {
        dir = Vector3.down;
    }
    //total time: 2.5
    IEnumerator StartSequence() {
        float initialDist = 1f;
        float initialSpeed = 3f;
        Vector3 endPos = dir*initialDist;
        float f = initialDist/initialSpeed;
        AnimationCurve curve = customCurveStart;

        StartCoroutine(Move(endPos,f, curve));

        yield return new WaitForSeconds(f+0.5f);

        //Allow popcorn units to take damage
        Popcorn[] enemies = GetComponentsInChildren<Popcorn>(false);
        for (int i = 0; i < enemies.Count<Popcorn>(); i++) enemies[i].ResetScaling();

        float finalSpeed = initialSpeed*2;
        endPos = dir*15f;
        f = 15f/finalSpeed;
        curve = customCurveEnd;

        StartCoroutine(Move(endPos,f, curve));

        yield return new WaitForSeconds(f);
        OnDeath();
    }

    private IEnumerator Move(Vector3 d, float movetime, AnimationCurve a) {
        Vector3 dir = d;
        Vector3 target = transform.position + dir;
        Vector3 start = transform.position;

        float dist = Vector3.Distance(target, start);

        Vector3 currentPosition = start;

        AnimationCurve curve = a;

        float t = 0;
        
        while (t < movetime) {
            currentPosition = Vector3.Lerp(start,target,curve.Evaluate(t/movetime));
            //Debug.Log(t);
            t += Time.deltaTime;

            transform.position = currentPosition;

            yield return new WaitForEndOfFrame();
        }
    }

    public override void OnDeath()
    {
        EnemyAI[] enemies = GetComponentsInChildren<EnemyAI>(false);
        for (int i = 1; i < enemies.Count<EnemyAI>(); i++) enemies[i].OnDeath();
    }
}
