using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public GameObject beam;

    private GameObject b;
    private GameObject a;

    public float LightBulletTime = 1f;
    public float LightBulletSpeed = 4f;
    public int LightBulletNo = 2;

    public float LightBeamTime = 3f;
    int LightBeamTics = 200;

    public float SpinPreWait = 2f;
    public float SpinRadius = 1f;
    public int SpinNo = 6;
    public float SpinBulletSpeed = 4f;
    public float SpinWait = 0.05f;
    public float SpinAngle = 45f;
    public int SpinCount = 12;

    public float CirclePreWait = 2f;
    public float CircleRadius = 1f;
    public int CircleNo = 12;
    public float CircleBulletSpeed = 4f;
    public float CircleWait = 1f;
    public int CircleRepeat = 10;

    float TwoBeamTime = 1.5f;
    int TwoBeamTics = 500;
    float TwoBeamMoveTime = 0.25f;

    public void LightAttackBullet() {
        StartCoroutine(LABullet());
    }

    IEnumerator LABullet() {
        BossIdle.instance.CanExit(false);
        Vector2 FP1 = transform.position + new Vector3(1f, 0, 1);
        Vector2 FP2 = transform.position + new Vector3(-1f, 0, 1);
        GameObject bullet;

        for (int i = 1; i <= LightBulletNo; i++) {
            Vector2 player = PlayerScript.instance.transform.position;

            bullet = BulletPoolScript.instance.GetBullet();
            bullet.transform.position = FP1;
            bullet.GetComponent<BulletMovement>().SetSpeed(LightBulletSpeed);
            bullet.GetComponent<BulletMovement>().SetDirection(player.x - FP1.x, player.y - FP1.y);
            bullet.SetActive(true);

            bullet = BulletPoolScript.instance.GetBullet();
            bullet.transform.position = FP2;
            bullet.GetComponent<BulletMovement>().SetSpeed(LightBulletSpeed);
            bullet.GetComponent<BulletMovement>().SetDirection(player.x - FP2.x, player.y - FP2.y);
            bullet.SetActive(true);

            yield return new WaitForSeconds(LightBulletTime);
        }

        BossIdle.instance.CanExit(true);
    }

    public void LightAttackBeam() {
        StartCoroutine(LABeam());
    }

    IEnumerator LABeam() {
        BossIdle.instance.CanExit(false);
        //Vector3 player = PlayerScript.instance.transform.position;
        //Vector2 FP = new Vector2(0,0);

        //FP = player;
        b = Instantiate(beam);
        Destroy(b, 5f);

        b.transform.position = transform.position;

        //Vector3 targetDir = player - transform.position;

        //targetDir = Quaternion.Euler(0,0,180) * targetDir;

        //Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, targetDir);

        //b.transform.Rotate(targetRotation.eulerAngles);
        b.transform.Rotate(0,0,-60);

        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < LightBeamTics; i++) {
            b.transform.Rotate(0,0,120f/LightBeamTics);
            yield return new WaitForSeconds(LightBeamTime/LightBeamTics);
        }

        for (int i = 0; i < LightBeamTics; i++) {
            b.transform.Rotate(0,0,-120f/LightBeamTics);
            yield return new WaitForSeconds(LightBeamTime/LightBeamTics);
        }

        yield return new WaitForSeconds(0.2f);
        Destroy(b);
        BossIdle.instance.CanExit(true);
    }

    public void Spin() {
        BossAttack.instance.CanExit(false);
        StartCoroutine(Spinning());
    }

    IEnumerator Spinning() {
        BossAttack.instance.CanExit(false);
        Vector2 FirePoint = new Vector2(1, 0);
        GameObject bullet;

        yield return new WaitForSeconds(SpinPreWait);

        for (int j = 0; j < SpinCount; j++) {

            for (int i = 0; i < SpinNo; i++) {
                bullet = BulletPoolScript.instance.GetBullet();

                bullet.transform.position = transform.position + (new Vector3(FirePoint.x, FirePoint.y, 1))*SpinRadius;

                //Set bullet speed
                bullet.GetComponent<BulletMovement>().SetSpeed(SpinBulletSpeed);

                //Rotate bullet.transform.pos to correct pos
                Vector2 dir = bullet.transform.position - transform.position;
                dir = Quaternion.Euler(0, 0, (360/SpinNo)*i)*dir;
                bullet.transform.position = transform.position + new Vector3(dir.x, dir.y, 0);
                bullet.GetComponent<BulletMovement>().SetDirection(dir);

                bullet.SetActive(true);
            }

            yield return new WaitForSeconds(SpinWait);
            FirePoint = Quaternion.Euler(0, 0, (SpinAngle/SpinCount)*j)*FirePoint;
        }

        yield return new WaitForSeconds(1.5f);
        BossAttack.instance.CanExit(true);
    }

    public void Circle() {
        StartCoroutine(Circles());
    }

    IEnumerator Circles() {
        BossAttack.instance.CanExit(false);
        Vector2 horizontal = new Vector2(1,0);
        Vector2 FirePoint = new Vector2(1, 0);
        GameObject bullet;

        yield return new WaitForSeconds(CirclePreWait);

        for (int j = 0; j < CircleRepeat; j++) {

            for (int i = 0; i < CircleNo; i++) {
                bullet = BulletPoolScript.instance.GetBullet();

                bullet.transform.position = transform.position + (new Vector3(FirePoint.x, FirePoint.y, 1))*CircleRadius;

                //Set bullet speed
                bullet.GetComponent<BulletMovement>().SetSpeed(CircleBulletSpeed);

                //Rotate bullet.transform.pos to correct pos
                Vector2 dir = bullet.transform.position - transform.position;
                dir = Quaternion.Euler(0, 0, (360/CircleNo)*i)*dir;
                bullet.transform.position = transform.position + new Vector3(dir.x, dir.y, 0);
                bullet.GetComponent<BulletMovement>().SetDirection(dir);

                bullet.SetActive(true);
            }

            if (j % 2 == 0) {
                yield return new WaitForSeconds(0.1f);
            } else {
                yield return new WaitForSeconds(0.8f);
            }

        }

        yield return new WaitForSeconds(1.5f);
        BossAttack.instance.CanExit(true);
    }

    public void TwoBeam() {
        StartCoroutine(TwoBeams());
    }

    IEnumerator TwoBeams() {
        BossIdle.instance.CanExit(false);

        //Move the boss to (0,5,1);
        Vector3 targetPos = new Vector3(0,5,1);

        Vector3 dir = targetPos - transform.position;

        Vector3 curPos = transform.position;

        for (int i = 0; i < 100; i++) {
            transform.position = transform.position + dir/100;
            yield return new WaitForSeconds(TwoBeamMoveTime/100);
        }

        //Spawn two lasers at -80/80 degrees. Rotate them anticlock/clock by 160
        a = Instantiate(beam);
        b = Instantiate(beam);
        //Destroy(a, TwoBeamTime + 0.5f);
        //Destroy(b, TwoBeamTime + 0.5f);
        a.transform.position = transform.position + new Vector3(0,0,0);
        b.transform.position = transform.position + new Vector3(0,0,0);
        a.transform.Rotate(0,0, 80);
        b.transform.Rotate(0,0,-80);

        for (int i = 0; i < TwoBeamTics; i++) {
            b.transform.Rotate(0,0,(160f)/TwoBeamTics);
            a.transform.Rotate(0,0,-(160f)/TwoBeamTics);
            yield return new WaitForSeconds(TwoBeamTime/TwoBeamTics);
        }

        for (int i = 0; i < TwoBeamTics; i++) {
            b.transform.Rotate(0,0,-(160f)/TwoBeamTics);
            a.transform.Rotate(0,0, (160f)/TwoBeamTics);
            yield return new WaitForSeconds(TwoBeamTime/TwoBeamTics);
        }

        yield return new WaitForSeconds(0.5f);

        Destroy(a);
        Destroy(b);

        targetPos = curPos;

        dir = targetPos - transform.position;

        for (int i = 0; i < 100; i++) {
            transform.position = transform.position + dir/100;
            yield return new WaitForSeconds(TwoBeamMoveTime/100);
        }

        yield return new WaitForSeconds(1.5f);
        BossAttack.instance.CanExit(true);
    }

    public void OnDeath() {
        if (b) {
            Destroy(b);
        }

        if (a) {
            Destroy(a);
        }

        transform.gameObject.SetActive(false);
    }
}
