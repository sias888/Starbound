using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    protected AnimationCurve curve;
    //public int EnemyType;
    /*
    public void SetEnemyType(int i) {
        //EnemyType = i;
    }*/

    public bool StartAI;
    protected float damageScaling = 1f;

    public virtual void TriggerAI(bool b) {
        StartAI = b;
        if (b == true)
            gameObject.GetComponent<EnemyHP>().Armor = false;
    }

    bool SideToSide = false;
    float SideToSideSpeed = 0f;
    public void SideToSideTrigger(bool b, float s) {
        //Debug.Log("triggered!");
        SideToSide = b;
        SideToSideSpeed = s;
    }

    public void SideToSideSwap() {
        //Debug.Log("amongus");
        SideToSideSpeed *= -1;
    }

    Vector2 velocity = new Vector2(0,0);

    virtual protected void FixedUpdate() {
        //Debug.Log(velocity);
        //SideToSideController
        if (!SideToSide) {
            velocity = new Vector2(0,0);
        }
        if (SideToSide) {
            velocity = new Vector2(SideToSideSpeed,0);
        }
        transform.position += (Vector3)velocity*Time.fixedDeltaTime;

    }



    public void Move(Vector2 target, float movetime) {
        if (gameObject.activeInHierarchy)
            StartCoroutine(MoveToLocation(target, movetime));
    }

    public void MoveAndStart(Vector2 target, float movetime) {
        if (gameObject.activeInHierarchy)
            StartCoroutine(MoveToLocationAndStart(target, movetime));
    }

    public IEnumerator MoveToLocationAndStart(Vector3 d, float movetime) {
        Vector3 dir = d;
        Vector3 target = transform.position + dir;
        Vector3 start = transform.position;

        float dist = Vector3.Distance(target, start);

        Vector3 currentPosition = start;

        curve = AnimationCurve.EaseInOut(0,0,1,1);

        float t = 0;
        
        while (t < movetime) {
            currentPosition = Vector3.Lerp(start,target,curve.Evaluate(t/movetime));
            //Debug.Log(t);
            t += Time.deltaTime;

            transform.position = currentPosition;

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.5f);
        TriggerAI(true);
        //StartAI = true;
    }

    protected IEnumerator MoveToLocation(Vector3 d, float movetime) {
        Vector3 dir = d;
        Vector3 target = transform.position + dir;
        Vector3 start = transform.position;

        float dist = Vector3.Distance(target, start);

        Vector3 currentPosition = start;

        curve = AnimationCurve.EaseInOut(0,0,1,1);

        float t = 0;
        
        while (t < movetime) {
            currentPosition = Vector3.Lerp(start,target,curve.Evaluate(t/movetime));
            //Debug.Log(t);
            t += Time.deltaTime;

            transform.position = currentPosition;

            yield return new WaitForEndOfFrame();
        }
    }

    public virtual void Shoot() {}

    public virtual void Stop() {}

    public GameObject explosion;
    protected float explosionModifier = 1;
    public virtual void OnDeath() {
        //Debug.Log("AMongus2??");
        StartAI = false;
        StopAllCoroutines();
        GameObject e = Instantiate(explosion, transform.position, Quaternion.identity);
        e.transform.localScale = new Vector3(3* explosionModifier,3* explosionModifier,1);
        transform.gameObject.SetActive(false);
        EnemyDeathEventHandler.instance.EnemyDeathTrigger(this.gameObject);
    }

}
