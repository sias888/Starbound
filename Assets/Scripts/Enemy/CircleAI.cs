using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAI : EnemyAI
{
    // Start is called before the first frame update
    public GameObject bullet;
    //public GameObject firePoint;

    public float ShootTime;
    public float bulletSpeed;
    public int numBullets;
    private GameObject b;
    //private bool canShoot = true;

    public AnimationCurve customCurve;

    // Start is called before the first frame update
    void Start()
    {
        TriggerAI(true);
        StartCoroutine(StartSequence());
    }

    IEnumerator StartSequence() {
        yield return new WaitForSeconds(0.5f);
        Vector3 endPos = Vector3.down*5f;
        float f = 1.5f;
        AnimationCurve curve = customCurve;

        StartCoroutine(Move(endPos,f, curve));

        yield return new WaitForSeconds(f);

        curve = AnimationCurve.Linear(0,0,1,1);
        endPos = Vector3.down*5f;
        f *= 2;
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
        base.OnDeath();
        for (int i = 0; i<numBullets; i++) {
                b = Instantiate(bullet);

                b.transform.position = transform.position;

                //Set bullet speed
                b.GetComponent<BulletMovement>().SetSpeed(bulletSpeed);

                //Rotate bullet.transform.pos to correct angle
                Vector2 dir = new Vector3(0,-1f,0);
                dir = Quaternion.Euler(0, 0, (360/numBullets)*i)*dir;
                b.transform.position = transform.position + new Vector3(dir.x, dir.y, 0);
                b.GetComponent<BulletMovement>().SetDirection(dir);

                //reset position to current position to make bullets spawn from center, rather than center+dir
                b.transform.position = transform.position;

                Destroy(b,7.5f);
        }
    }
}
