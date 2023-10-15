using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

//using JetBrains.Annotations;
//using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

public class BossAI : EnemyAI
{
    public static BossAI instance;

    void Awake() {
        instance = this;
        firePopcorn = true;
        //popcornPatterns.Add(Level1EnemyController.instance.PPT);
    }

    public GameObject beam;
    public GameObject ExclaimPrefab;

    [SerializeField]
    private float ExclaimTime = 0.8f;
    //private float ExclaimHeight = 1f;
    //private bool ExclaimDone = false;

    private GameObject b;
    private GameObject a;
    private GameObject c;
    private GameObject d;

    private GameObject e;

    private float LightBulletTime = 0.25f;
    private float LightBulletSpeed = 4f;
    private int LightBulletNo = 4;

    private float LightBeamTime = 0.75f*2;
    private int LightBeamTics = 250;

    private float LBBWaitTime = 1f;

    //private float SpinPreWait = 0.5f;
    private float SpinRadius = 1f;
    private int SpinNo = 3;
    private float SpinBulletSpeed = 3f;
    private float SpinWait = 0.1f;
    private float SpinAngle = 30f;
    private int SpinCount = 50;

    //private float CirclePreWait = 2f;
    private float CircleRadius = 1f;
    private int CircleNo = 36/2;
    private float CircleBulletSpeed = 2.5f;
    private float CircleWait = 1f * 0.7f;
    private int CircleRepeat = 5;

    float TwoBeamTime = 2f;
    int TwoBeamTics = 500;
    float TwoBeamMoveTime = 0.25f;

    int BSticks = 750;
    float BStime = 4f*1.5f;

    int BSBticks = 500;
    float BSBtime = 5f;

    IEnumerator Exclaim() {
        //ExclaimDone = false;
        e = Instantiate(ExclaimPrefab, transform.GetChild(4).transform);
        //Debug.Log(e.activeInHierarchy == true);
        yield return new WaitForSeconds(ExclaimTime);
        //StartCoroutine(f());
        //ExclaimDone = true;
    }

    float DrillCoolDown = 3f;
    public GameObject drill;
    public void ShootDrills() {
        Vector3 FP1 = transform.position;// + new Vector3(1f, 1f, 0);
        Vector3 FP2 = transform.position;// + new Vector3(-1f, 1f, 0);

        //FP1.Normalize();
        //FP2.Normalize();

        GameObject d1 = Instantiate(drill);
        GameObject d2 = Instantiate(drill);

        d1.transform.position = FP1 + Quaternion.Euler(0,0,-66)*Vector3.up*CircleRadius;
        d2.transform.position = FP2 + Quaternion.Euler(0,0, 66)*Vector3.up*CircleRadius;

        d1.transform.Rotate(new Vector3(0, 0, -66));
        d2.transform.Rotate(new Vector3(0, 0, 66));

        d1.SetActive(true);
        d2.SetActive(true);
    }

    private bool canShootDrill;
    private IEnumerator DrillRepeat() {
        canShootDrill = false;
        ShootDrills();
        yield return new WaitForSeconds(DrillCoolDown);
        canShootDrill = true;
    }

    public void TriggerDrills(bool b) {
        canShootDrill = b;
    }

    public bool firePopcorn = true;
    public bool startFirePopcorn = false;
    private List<IEnumerable> popcornPatterns = new List<IEnumerable>();
    public IEnumerator ShootPopcorn() {
        firePopcorn = false;
        popcornFire = StartCoroutine(Level1EnemyController.instance.PopcornPatternAngleInwards());
        yield return new WaitForSeconds(10f);
        firePopcorn = true;
    }

    private void Update() {
        if (firePopcorn && startFirePopcorn) {
            StartCoroutine(ShootPopcorn());
        }
    }

    public void StartXBullets() {
        StartCoroutine(XBullets());
    }

    public IEnumerator XBullets() {
        Vector2 FP = transform.position;
        GameObject bullet;

        for (int j = 0; j < 3; j++) {
            for (int i = 0; i < 8; i++) {
                Vector2 direction = new Vector2(0,-1);
                bullet = BulletPoolScript.instance.GetBullet();
                bullet.transform.position = FP;
                bullet.GetComponent<BulletMovement>().SetSpeed(LightBulletSpeed);
                bullet.GetComponent<BulletMovement>().SetDirection(Quaternion.Euler(0,0,45*i)*direction);

                bullet.SetActive(true);
            }
            yield return new WaitForSeconds(LightBulletTime/2);
        }
    }

    public void LightAttackBullet() {
        StartCoroutine(LABullet());
    }

    IEnumerator LABullet() {
        Debug.Log("Light Attack Bullet");
        yield return Exclaim();
        BossIdle.instance.CanExit(false);
        Vector2 FP1 = transform.position + new Vector3(1f, 0, 1);
        Vector2 FP2 = transform.position + new Vector3(-1f, 0, 1);
        GameObject bullet;

        int LBN = Random.Range(0,4); //Number of bullets to be fired

        for (int i = 1; i <= LightBulletNo + LBN; i++) {
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
        Destroy(e);
        BossIdle.instance.CanExit(true);
    }

    public void LightAttackBeam() {
        float xpos = transform.position.x;

        if (xpos > 1) {
            StartCoroutine(LABEAM());
        }
        else if (xpos < 1) {
            StartCoroutine(LABEAMFLIPPED());
        } else {
            int r = Random.Range(0,2);
            if (r == 0) StartCoroutine(LABEAM());
            else StartCoroutine(LABEAMFLIPPED());
        }


    }


    IEnumerator LABeamFlipped() {
        yield return Exclaim();
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
        b.transform.Rotate(0,0,60);

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < LightBeamTics; i++) {
            b.transform.Rotate(0,0,-120f/LightBeamTics);
            yield return new WaitForSeconds(LightBeamTime/LightBeamTics);
        }

        for (int i = 0; i < LightBeamTics; i++) {
            b.transform.Rotate(0,0,120f/LightBeamTics);
            yield return new WaitForSeconds(LightBeamTime/LightBeamTics);
        }

        //yield return new WaitForSeconds(0.2f);
        //Destroy(b, 0.25f);
        b.GetComponent<Animator>().Play("ThickLaserFinish");
        Destroy(e);
        BossIdle.instance.CanExit(true);
    }
    IEnumerator LABeam() {
        Debug.Log("Light Attack Beam");
        yield return Exclaim();
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

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < LightBeamTics; i++) {
            b.transform.Rotate(0,0,120f/LightBeamTics);
            yield return new WaitForSeconds(LightBeamTime/LightBeamTics);
        }

        for (int i = 0; i < LightBeamTics; i++) {
            b.transform.Rotate(0,0,-120f/LightBeamTics);
            yield return new WaitForSeconds(LightBeamTime/LightBeamTics);
        }

        //yield return new WaitForSeconds(0.2f);
        //Destroy(b, 0.25f);
        b.GetComponent<Animator>().Play("ThickLaserFinish");
        Destroy(e);
        BossIdle.instance.CanExit(true);
    }

    IEnumerator LABEAM() {
        yield return Exclaim();
        BossIdle.instance.CanExit(false);
        b = Instantiate(beam);
        Destroy(b, 5f);

        b.transform.position = transform.position;
        b.transform.Rotate(0,0,-60);

        yield return new WaitForSeconds(0.5f);

        AnimationCurve Acurve = AnimationCurve.Linear(0,0,1,1);

        float t = 0;
        float currAngle = 0;
        while (t < LightBeamTime) {
            currAngle = Mathf.Lerp(0,60,Time.deltaTime * LightBeamTime);
            t += Time.deltaTime;

            b.transform.Rotate(0,0,currAngle);

            yield return new WaitForEndOfFrame();
        }

        t = 0;
        currAngle = 0;
        while (t < LightBeamTime) {
            currAngle = Mathf.Lerp(0,60,Time.deltaTime * LightBeamTime);
            t += Time.deltaTime;

            b.transform.Rotate(0,0,-currAngle);

            yield return new WaitForEndOfFrame();
        }

        b.GetComponent<Animator>().Play("ThickLaserFinish");
        if (e) Destroy(e);
        BossIdle.instance.CanExit(true);
    }

    IEnumerator LABEAMFLIPPED() {
        yield return Exclaim();
        BossIdle.instance.CanExit(false);
        b = Instantiate(beam);
        Destroy(b, LightBeamTime*2+1.5f);

        b.transform.position = transform.position;
        b.transform.Rotate(0,0,60);

        yield return new WaitForSeconds(0.5f);

        AnimationCurve Acurve = AnimationCurve.Linear(0,0,1,1);

        float t = 0;
        float currAngle = 0;
        while (t < LightBeamTime) {
            currAngle = Mathf.Lerp(0,-60,Time.deltaTime * LightBeamTime);
            t += Time.deltaTime;

            b.transform.Rotate(0,0,currAngle);

            yield return new WaitForEndOfFrame();
        }

        t = 0;
        currAngle = 0;
        while (t < LightBeamTime) {
            currAngle = Mathf.Lerp(0,-60,Time.deltaTime * LightBeamTime);
            t += Time.deltaTime;

            b.transform.Rotate(0,0,-currAngle);

            yield return new WaitForEndOfFrame();
        }

        b.GetComponent<Animator>().Play("ThickLaserFinish");
        if (e) Destroy(e);
        BossIdle.instance.CanExit(true);
    }

    public void LightBeamBlast() {
        StartCoroutine(LBB());
    }

    IEnumerator LBB() {
        Debug.Log("Light Beam Blast");
        yield return Exclaim();
        BossIdle.instance.CanExit(false);

        int r = Random.Range(5,8);
        //BeamLoopAudio.instance.Mute(true);

        for (int i = 0; i<r; i++) {
            Vector3 player = PlayerScript.instance.transform.position;
            b = Instantiate(beam);
            Destroy(b,LBBWaitTime*1.2f);
            b.transform.position = transform.position;

            Vector3 targetDir = player - transform.position;
            targetDir = Quaternion.Euler(0,0,180) * targetDir;
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, targetDir);
            b.transform.Rotate(targetRotation.eulerAngles);

            StartCoroutine(LBBHelper(b));

            yield return new WaitForSeconds(LBBWaitTime*0.75f);
            //Destroy(b,0.5f);
        }
        Destroy(e);
        yield return new WaitForSeconds(0.5f);
        //BeamLoopAudio.instance.Mute(false);
        BossIdle.instance.CanExit(true);
    }

    IEnumerator LBBHelper(GameObject b) {
        yield return new WaitForSeconds(LBBWaitTime);
        if (b) b.GetComponent<Animator>().Play("ThickLaserFinish");
    }
    

    public void Spin() {
        StartCoroutine(Spinning());
    }

    IEnumerator Spinning() {
        Debug.Log("Bullet Spin");
        yield return Exclaim();
        BossAttack.instance.CanExit(false);
        Vector2 FirePoint = new Vector2(1, 0);
        GameObject bullet;

        //yield return new WaitForSeconds(SpinPreWait);

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

        //yield return new WaitForSeconds(1.5f);
        Destroy(e);
        BossAttack.instance.CanExit(true);
    }

    public void Circle() {
        StartCoroutine(Circles());
    }

    IEnumerator Circles() {
        Debug.Log("Bullet Rings");
        yield return Exclaim();
        BossAttack.instance.CanExit(false);
        Vector2 horizontal = new Vector2(1,0);
        Vector2 FirePoint = new Vector2(1, 0);
        GameObject bullet;

        //yield return new WaitForSeconds(CirclePreWait);
        int offset = 0;
        for (int j = 0; j < CircleRepeat*2; j++) {
            for (int i = 0; i < CircleNo; i++) {
                bullet = BulletPoolScript.instance.GetBullet();

                bullet.transform.position = transform.position + (new Vector3(FirePoint.x, FirePoint.y, 1))*CircleRadius;

                //Set bullet speed
                bullet.GetComponent<BulletMovement>().SetSpeed(CircleBulletSpeed);

                //Rotate bullet.transform.pos to correct pos
                Vector2 dir = bullet.transform.position - transform.position;
                dir = Quaternion.Euler(0, 0, (360/CircleNo)*i + offset*10)*dir;
                bullet.transform.position = transform.position + new Vector3(dir.x, dir.y, 0);
                bullet.GetComponent<BulletMovement>().SetDirection(dir);

                bullet.SetActive(true);
            }

            if (j % 2 == 0) {
                yield return new WaitForSeconds(0.2f);
            } else {
                offset = Random.Range(-4,5);
                yield return new WaitForSeconds(CircleWait);
            }

        }

        //yield return new WaitForSeconds(1.5f);
        Destroy(e);
        BossAttack.instance.CanExit(true);
    }

    public void TwoBeam() {
        StartCoroutine(TwoBeams());
    }

    IEnumerator TwoBeams() {
        Debug.Log("Two Beams");
        yield return Exclaim();
        BossIdle.instance.CanExit(false);

        //Move the boss to (0,5,1);
        Vector3 targetPos = new Vector3(0,3.5f,1);

        Vector3 dir = targetPos - transform.position;

        Vector3 curPos = transform.position;

        /*
        for (int i = 0; i < 100; i++) {
            transform.position = transform.position + dir/100;
            yield return new WaitForSeconds(TwoBeamMoveTime/100);
        }*/

        yield return StartCoroutine(MoveToLocation(dir, TwoBeamMoveTime*1.5f));

        //Spawn two lasers at -80/80 degrees. Rotate them anticlock/clock by 160
        a = Instantiate(beam);
        b = Instantiate(beam);
        //Destroy(a, TwoBeamTime + 0.5f);
        //Destroy(b, TwoBeamTime + 0.5f);
        a.transform.position = transform.position + new Vector3(0,0,0);
        b.transform.position = transform.position + new Vector3(0,0,0);
        a.transform.Rotate(0,0, 80);
        b.transform.Rotate(0,0,-80);

        yield return new WaitForSeconds(0.5f);

        /*
        for (int i = 0; i < TwoBeamTics; i++) {
            b.transform.Rotate(0,0,(160f)/TwoBeamTics);
            a.transform.Rotate(0,0,-(160f)/TwoBeamTics);
            yield return new WaitForSeconds(TwoBeamTime/TwoBeamTics);
        }*/

        float t = 0;
        float currAngle = 0;
        while (t < TwoBeamTime) {
            currAngle = Mathf.Lerp(0,160f,t/TwoBeamTime);
            t += Time.deltaTime;

            a.transform.rotation = Quaternion.AngleAxis(currAngle-80 , Vector3.forward);
            b.transform.rotation = Quaternion.AngleAxis(-currAngle+80 , Vector3.forward);

            yield return new WaitForEndOfFrame();
        }

        /*
        for (int i = 0; i < TwoBeamTics; i++) {
            b.transform.Rotate(0,0,-(160f)/TwoBeamTics);
            a.transform.Rotate(0,0, (160f)/TwoBeamTics);
            yield return new WaitForSeconds(TwoBeamTime/TwoBeamTics);
        }*/


        t = 0;
        currAngle = 0;
        while (t < TwoBeamTime) {
            currAngle = Mathf.Lerp(0,-160f,t/TwoBeamTime);
            t += Time.deltaTime;

            a.transform.rotation = Quaternion.AngleAxis(currAngle+80 , Vector3.forward);
            b.transform.rotation = Quaternion.AngleAxis(-currAngle-80 , Vector3.forward);

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.5f);

        //Destroy(a);
        //Destroy(b);

        a.GetComponent<Animator>().Play("ThickLaserFinish");
        b.GetComponent<Animator>().Play("ThickLaserFinish");

        targetPos = curPos;

        dir = targetPos - transform.position;

        /*
        for (int i = 0; i < 100; i++) {
            transform.position = transform.position + dir/100;
            yield return new WaitForSeconds(TwoBeamMoveTime/100);
        }*/

        yield return StartCoroutine(MoveToLocation(dir, TwoBeamMoveTime*1.5f));

        //yield return new WaitForSeconds(1.5f);
        Destroy(e);
        BossAttack.instance.CanExit(true);
    }

    public void BeamSpin() {
        StartCoroutine(BS());
    }

    IEnumerator BS() {
        Debug.Log("Beam Spin");
        yield return Exclaim();
        BossAttack.instance.CanExit(false);
        Vector2 FirePoint = new Vector2(1, 0);

        a = Instantiate(beam);
        b = Instantiate(beam);
        c = Instantiate(beam);
        d = Instantiate(beam);

        a.transform.position = transform.position + new Vector3(0,0,0);
        b.transform.position = transform.position + new Vector3(0,0,0);
        c.transform.position = transform.position + new Vector3(0,0,0);
        d.transform.position = transform.position + new Vector3(0,0,0);
        a.transform.RotateAround(transform.position, Vector3.forward, 0);
        b.transform.RotateAround(transform.position, Vector3.forward, 90);
        c.transform.RotateAround(transform.position, Vector3.forward, 180);
        d.transform.RotateAround(transform.position, Vector3.forward, 270);

        a.transform.rotation = Quaternion.AngleAxis(0+45    , Vector3.forward);
        b.transform.rotation = Quaternion.AngleAxis(0+90+45 , Vector3.forward);
        c.transform.rotation = Quaternion.AngleAxis(0+180+45, Vector3.forward);
        d.transform.rotation = Quaternion.AngleAxis(0+270+45, Vector3.forward);

        yield return new WaitForSeconds(0.5f);
        /*
        for (int i = 0; i < BSticks; i++) {
            a.transform.Rotate(0,0,(360f)/BSticks);
            b.transform.Rotate(0,0,(360f)/BSticks);
            c.transform.Rotate(0,0,(360f)/BSticks);
            d.transform.Rotate(0,0,(360f)/BSticks);
            yield return new WaitForSeconds(BStime/BSticks);
        }*/

        AnimationCurve Acurve = AnimationCurve.Linear(0,0,1,1);


        float t = 0;
        float currAngle = 0;
        while (t < BStime) {
            currAngle = Mathf.Lerp(0,360f,t/BStime);
            t += Time.deltaTime;

            a.transform.rotation = Quaternion.AngleAxis(currAngle+45    , Vector3.forward);
            b.transform.rotation = Quaternion.AngleAxis(currAngle+90+45 , Vector3.forward);
            c.transform.rotation = Quaternion.AngleAxis(currAngle+180+45, Vector3.forward);
            d.transform.rotation = Quaternion.AngleAxis(currAngle+270+45, Vector3.forward);

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.5f);

        a.GetComponent<Animator>().Play("ThickLaserFinish");
        b.GetComponent<Animator>().Play("ThickLaserFinish");
        c.GetComponent<Animator>().Play("ThickLaserFinish");
        d.GetComponent<Animator>().Play("ThickLaserFinish");

        /*
        Destroy(a);
        Destroy(b);
        Destroy(c);
        Destroy(d);
        */
        Destroy(e);
        BossAttack.instance.CanExit(true);
    }

    public void BeamSpinBullets() {
        Debug.Log("Beam Spin Bullets");
        if (transform.position.x >= 0) StartCoroutine(BSBeam());
        else StartCoroutine(BSBeamReverse());

        StartCoroutine(BSBullets());
    }

    IEnumerator BSBullets() {
        yield return Exclaim();
        yield return new WaitForSeconds(0.8f);
        Vector2 FirePoint = new Vector2(1, 0);
        GameObject bullet;

        for (int j = 0; j < 50; j++) {

            for (int i = 0; i < 3; i++) {
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

            yield return new WaitForSeconds(BSBtime/50);
            FirePoint = Quaternion.Euler(0, 0, (360/10)*j)*FirePoint;
        }

    }

    IEnumerator BSBeamReverse() {
        BossAttack.instance.CanExit(false);
        yield return new WaitForSeconds(0.8f);
        Vector2 FirePoint = new Vector2(1, 0);

        a = Instantiate(beam);
        b = Instantiate(beam);
        c = Instantiate(beam);

        a.transform.position = transform.position + new Vector3(0,0,0);
        b.transform.position = transform.position + new Vector3(0,0,0);
        c.transform.position = transform.position + new Vector3(0,0,0);
        a.transform.Rotate(0,0,0);
        b.transform.Rotate(0,0,120);
        c.transform.Rotate(0,0,240);

        yield return new WaitForSeconds(0.5f);

        /*
        for (int i = 0; i < BSBticks; i++) {
            a.transform.Rotate(0,0,(180f)/BSBticks);
            b.transform.Rotate(0,0,(180f)/BSBticks);
            c.transform.Rotate(0,0,(180f)/BSBticks);
            yield return new WaitForSeconds(BSBtime/BSBticks);
        }*/

        float t = 0;
        float currAngle = 0;
        while (t < BSBtime) {
            currAngle = Mathf.Lerp(0,180f,t/BSBtime);
            t += Time.deltaTime;

            a.transform.rotation = Quaternion.AngleAxis(-currAngle+0 , Vector3.forward);
            b.transform.rotation = Quaternion.AngleAxis(-currAngle+120, Vector3.forward);
            c.transform.rotation = Quaternion.AngleAxis(-currAngle+240, Vector3.forward);

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.5f);

        a.GetComponent<Animator>().Play("ThickLaserFinish");
        b.GetComponent<Animator>().Play("ThickLaserFinish");
        c.GetComponent<Animator>().Play("ThickLaserFinish");
        //d.GetComponent<Animator>().Play("ThickLaserFinish");

        //Destroy(a);
        //Destroy(b);
        //Destroy(c);
        //Destroy(d);

        Destroy(e);
        BossAttack.instance.CanExit(true);
    }

    IEnumerator BSBeam() {
        BossAttack.instance.CanExit(false);
        yield return new WaitForSeconds(0.8f);
        Vector2 FirePoint = new Vector2(1, 0);

        a = Instantiate(beam);
        b = Instantiate(beam);
        c = Instantiate(beam);

        a.transform.position = transform.position + new Vector3(0,0,0);
        b.transform.position = transform.position + new Vector3(0,0,0);
        c.transform.position = transform.position + new Vector3(0,0,0);
        a.transform.Rotate(0,0,0);
        b.transform.Rotate(0,0,120);
        c.transform.Rotate(0,0,240);

        yield return new WaitForSeconds(0.5f);

        /*
        for (int i = 0; i < BSBticks; i++) {
            a.transform.Rotate(0,0,(180f)/BSBticks);
            b.transform.Rotate(0,0,(180f)/BSBticks);
            c.transform.Rotate(0,0,(180f)/BSBticks);
            yield return new WaitForSeconds(BSBtime/BSBticks);
        }*/

        float t = 0;
        float currAngle = 0;
        while (t < BSBtime) {
            currAngle = Mathf.Lerp(0,180f,t/BSBtime);
            t += Time.deltaTime;

            a.transform.rotation = Quaternion.AngleAxis(currAngle+0 , Vector3.forward);
            b.transform.rotation = Quaternion.AngleAxis(currAngle+120, Vector3.forward);
            c.transform.rotation = Quaternion.AngleAxis(currAngle+240, Vector3.forward);

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.5f);

        a.GetComponent<Animator>().Play("ThickLaserFinish");
        b.GetComponent<Animator>().Play("ThickLaserFinish");
        c.GetComponent<Animator>().Play("ThickLaserFinish");
        //d.GetComponent<Animator>().Play("ThickLaserFinish");

        //Destroy(a);
        //Destroy(b);
        //Destroy(c);
        //Destroy(d);

        Destroy(e);
        BossAttack.instance.CanExit(true);
    }

    override public void OnDeath() {
        //transform.gameObject.SetActive(false);
    }

    public void DeathHelper() {
        StopAllCoroutines();

        if (a) a.GetComponent<Animator>().Play("ThickLaserFinish");;

        if (b) b.GetComponent<Animator>().Play("ThickLaserFinish");;

        if (c) c.GetComponent<Animator>().Play("ThickLaserFinish");;

        if (d) d.GetComponent<Animator>().Play("ThickLaserFinish");;

        if (e) Destroy(e);

        transform.GetChild(1).transform.gameObject.SetActive(false);
        transform.GetChild(2).transform.gameObject.SetActive(false);

        StartCoroutine(deathhelper());
    }

    public GameObject Explosion;

    Coroutine popcornFire;
    IEnumerator deathhelper() {
        startFirePopcorn = false;
        if (popcornFire != null) StopCoroutine(popcornFire);
        foreach(GameObject enemy in Level1EnemyController.instance.strayEnemies) {
            if (enemy.activeInHierarchy) enemy.GetComponent<EnemyAI>().OnDeath();
        }
        for (int i = 0; i < 25; i++) {
            Transform t = transform.GetChild(5).transform;

            float xoffset = Random.Range(-1,2) * 1f;
            float yoffset = Random.Range(-1,2) * 1f;

            GameObject e = Instantiate(Explosion, t);
            EnemyDieAudio.instance.PlayClip();
            e.transform.position = e.transform.position + new Vector3(xoffset,yoffset,0);
            e.transform.localScale = new Vector3(1.5f,1.5f,1);
            yield return new WaitForSeconds(0.05f);
            Destroy(e, 5f);
        }
        //yield return new WaitForSeconds(0.2f);
        PatternCompletionEventHandler.instance.PatternCompleteTrigger(gameObject);
    }

}
