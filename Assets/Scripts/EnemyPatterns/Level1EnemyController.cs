using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.U2D;

public class Level1EnemyController : MonoBehaviour
{
    public static Level1EnemyController instance;
    public GameObject threeEnemies;
    public GameObject oneBasicEnemy;
    public GameObject twoBasicEnemies;

    public GameObject twoSideToSide;
    public GameObject teleporter;

    public GameObject Boss;
    public GameObject BossHPBar;

    [SerializeField]
    private int phase = -1;

    public bool start = false;
    //private bool levelEnd = false;
    GameObject ep = null;

    List<GameObject> oldEp;
    //GameObject tp = null;
    int waveCount = 0;

    int bossPhase = 4;

    List<GameObject> patterns;

    public GameObject circleEnemy;
    public GameObject laserElite;
    public GameObject Popcorn;

    public List<GameObject> strayEnemies = new List<GameObject>();

    List<Vector3> lanes;

    private void Awake() {
        instance = this;
        lanes = new List<Vector3>();
        for (int i = 0; i < 3; i++) {
            lanes.Add(new Vector3());
            lanes[i] = new Vector3(-2f + i*2, 5.5f, 0);
        }

        PatternCompletionEventHandler.instance.onPatternCompletion += OnPatternComplete;
        oldEp = new List<GameObject>();

        patterns = new List<GameObject>() {oneBasicEnemy, twoBasicEnemies, twoSideToSide, threeEnemies};
    }
    private void Update() {
        //Coroutine st;
        //st = null;
        if (start) {

            //1 Enemy
            if (phase == -1) {
                start = false;
                ep = Instantiate(oneBasicEnemy);
                ep.SetActive(true);
                oldEp.Add(ep);
                ep.GetComponent<SimpleEnemyPattern>().start = true;
                waveCount++;
                ep = null;
                //phase++;
            }

            //2 Enemies
            if (phase == 0) {
                start = false;
                ep = Instantiate(twoBasicEnemies);
                ep.SetActive(true);
                oldEp.Add(ep);
                ep.GetComponent<SimpleEnemyPattern>().start = true;
                waveCount++;
                ep = null;
            }

            //3 Enemies
            if (phase == 1) {

                if (waveCount >= 3){
                    GameObject enemy = Instantiate(circleEnemy);
                    int r = Random.Range(0,lanes.Count);
                    enemy.transform.position = lanes[r];
                    enemy.SetActive(true);
                    strayEnemies.Add(enemy);
                }
                start = false;
                ep = Instantiate(threeEnemies);
                ep.SetActive(true);
                oldEp.Add(ep);
                ep.GetComponent<SimpleEnemyPattern>().start = true;
                waveCount++;
                ep = null;
            }

            //2 Moving enemies and mines
            if (phase == 2) {
                if (waveCount >= 0){
                    GameObject enemy = Instantiate(circleEnemy);
                    int r = Random.Range(0,lanes.Count);
                    enemy.transform.position = lanes[r];
                    enemy.SetActive(true);
                    strayEnemies.Add(enemy);
                }

                if (waveCount%2 == 0){
                    GameObject enemy = Instantiate(circleEnemy);
                    int r = Random.Range(0,lanes.Count);
                    enemy.transform.position = lanes[r];
                    enemy.SetActive(true);
                    strayEnemies.Add(enemy);
                }
                start = false;
                ep = Instantiate(twoSideToSide);
                ep.SetActive(true);
                oldEp.Add(ep);
                ep.GetComponent<SimpleEnemyPattern>().start = true;
                waveCount++;
                ep = null;
            }

            //2 enemies or 3 + 1 or 2 mines
            if (phase == 3) {

                if (waveCount >= 0 && waveCount != 20){
                    GameObject enemy = Instantiate(circleEnemy);
                    int r = Random.Range(0,lanes.Count);
                    enemy.transform.position = lanes[r];
                    enemy.SetActive(true);
                    strayEnemies.Add(enemy);
                }

                if (waveCount >= 15 && waveCount != 20){
                    GameObject enemy = Instantiate(circleEnemy);
                    int r = Random.Range(0,lanes.Count);
                    enemy.transform.position = lanes[r];
                    enemy.SetActive(true);
                    strayEnemies.Add(enemy);
                }

                if (waveCount%2 == 0 && waveCount != 20){
                    GameObject enemy = Instantiate(circleEnemy);
                    int r = Random.Range(0,lanes.Count);
                    enemy.transform.position = lanes[r];
                    enemy.SetActive(true);
                    strayEnemies.Add(enemy);
                }

                if (waveCount%3 == 2 && waveCount != 20){
                    StartCoroutine(SpawnPopcorn(4,0));
                    StartCoroutine(SpawnPopcorn(4,2));
                }

                if (waveCount%4 == 3 && waveCount != 20) {
                    StartCoroutine(PopcornPatternX(20));
                }

                if (waveCount == 5) {
                    GameObject enemy = Instantiate(laserElite);
                    int r = Random.Range(0,lanes.Count);
                    enemy.transform.position = lanes[r];
                    enemy.SetActive(true);
                    StartCoroutine(enemy.GetComponent<EnemyAI>().MoveToLocationAndStart(Vector3.down*5, 3));
                    strayEnemies.Add(enemy);
                }

                if (waveCount == 13) {
                    GameObject enemy = Instantiate(laserElite);
                    enemy.transform.position = lanes[0];
                    enemy.SetActive(true);
                    StartCoroutine(enemy.GetComponent<EnemyAI>().MoveToLocationAndStart(Vector3.down*2, 3));
                    enemy.GetComponent<LaserMiniboss>().SetSpinSpeed(20);
                    strayEnemies.Add(enemy);

                    enemy = Instantiate(laserElite);
                    enemy.transform.position = lanes[lanes.Count-1];
                    enemy.SetActive(true);
                    StartCoroutine(enemy.GetComponent<EnemyAI>().MoveToLocationAndStart(Vector3.down*2, 3));
                    enemy.GetComponent<LaserMiniboss>().SetSpinSpeed(-20);
                    strayEnemies.Add(enemy);
                }

                start = false;
                int rand = Random.Range(2,4);
                ep = Instantiate(patterns[rand]);
                ep.SetActive(true);
                oldEp.Add(ep);
                ep.GetComponent<SimpleEnemyPattern>().start = true;
                waveCount++;
                ep = null;
            }

            //Boss
            if (phase == bossPhase) {
                start = false;
                if (Boss != null) {
                    Boss.SetActive(true);
                    //BossHPBar.SetActive(true);
                }
                //phase++;
            }

            if (phase == bossPhase+1) {
                ScoreManager.instance.canDeplete = false;
                teleporter.SetActive(true);
                phase++;
            }
        }
    }



    IEnumerator WaitForNextSpawn(float t) {
        yield return new WaitForSeconds(t);
        phase++;
        start = true;
        ScoreManager.instance.canDeplete = true;
    }

    void OnPatternComplete(GameObject g) {

        //1 enemy
        if (phase == -1) {
            Destroy(g);
            if (waveCount >= 10) {
                waveCount = 0;
                StartCoroutine(SpawnPopcorn(4,1));
                StartCoroutine(WaitForNextSpawn(3f));
            } else start = true;
            return;
        }

        //2 enemies
        if (phase == 0) {
            //Debug.Log(oldEp);
            Destroy(g);
            if (waveCount >= 10) {
                waveCount = 0;
                StartCoroutine(PopcornPatternTriangle());
                StartCoroutine(WaitForNextSpawn(popcornTime*5 + 2.5f));
            } else start = true;
            return;
        }

        //3 enemies
        if (phase == 1) {
            //StartCoroutine(WaitForNextSpawn());
            Destroy(g);
            if (waveCount >= 5) {
                waveCount = 0;
                StartCoroutine(PopcornPatternX(20));
                StartCoroutine(SpawnPopcorn(5,1));
                StartCoroutine(WaitForNextSpawn(2.5f+popcornTime*5));
            } else start = true;
            return;
        }

        if (phase == 2) {
            //StartCoroutine(WaitForNextSpawn());
            Destroy(g);
            if (waveCount >= 5) {
                waveCount = 0;
                StartCoroutine(PopcornPatternRightLeft());
                StartCoroutine(WaitForNextSpawn(7f));
            } else start = true;
            return;
        }

        if (phase == 3) {
            //StartCoroutine(WaitForNextSpawn());
            Destroy(g);
            if (waveCount >= 20) {
                waveCount = 0;
                StartCoroutine(WaitForNextSpawn(1.75f));
                ScoreManager.instance.canDeplete = false;
                foreach(GameObject enemy in strayEnemies) {
                    if (enemy.activeInHierarchy) enemy.GetComponent<EnemyAI>().OnDeath();
                }
            } else start = true;
            return;
        }

        if (phase == bossPhase) {
            ScoreManager.instance.canDeplete = false;
            waveCount = 0;
            StartCoroutine(WaitForNextSpawn(1.75f));
        }
    }

    public float popcornTime = 0.15f;
    IEnumerator SpawnPopcorn(int n, int lane) {
        for (int i = 0; i<n; i++) {
            GameObject g =  Instantiate(Popcorn);
            g.SetActive(true);
            g.transform.position = lanes[lane];
            g.GetComponent<PopcornGroup>().dir = Vector3.down;
            g.GetComponent<EnemyAI>().TriggerAI(true);
            strayEnemies.Add(g);
            yield return new WaitForSeconds(popcornTime);
        }
    }
    //total time: 5*popcornwait + 2.5 = 3.25
    public IEnumerator PopcornPatternTriangle() {
        StartCoroutine(SpawnPopcorn(5,1));
        yield return new WaitForSeconds(popcornTime);
        StartCoroutine(SpawnPopcorn(4,0));
        StartCoroutine(SpawnPopcorn(4,2));
    }
    public IEnumerable<float> PPT() {
        StartCoroutine(PopcornPatternTriangle());
        yield return 3.25f;
    }

    //total time: 15*popcornTime+2.5 = 4.75
    public IEnumerator PopcornPatternOutToIn() {
        StartCoroutine(SpawnPopcorn(10,0));
        StartCoroutine(SpawnPopcorn(10,2));
        yield return new WaitForSeconds(popcornTime*5);
        StartCoroutine(SpawnPopcorn(10,1));
    }

    //Time:  = 16*popcornTime + 2.5 = 7
    public IEnumerator PopcornPatternRightLeft() {
        StartCoroutine(SpawnPopcorn(10,2));
        yield return new WaitForSeconds(popcornTime*11);
        StartCoroutine(SpawnPopcorn(10,1));
        yield return new WaitForSeconds(popcornTime*11);
        StartCoroutine(SpawnPopcorn(10,0));
    }
    public IEnumerable<float> R2L() {
        StartCoroutine(PopcornPatternRightLeft());
        yield return 7f;
    }

    //Time:  = 16*popcornTime + 2.5 = 7
    public IEnumerator PopcornPatternLeftRight() {
        StartCoroutine(SpawnPopcorn(10,0));
        yield return new WaitForSeconds(popcornTime*11);
        StartCoroutine(SpawnPopcorn(10,1));
        yield return new WaitForSeconds(popcornTime*11);
        StartCoroutine(SpawnPopcorn(10,2));
    }

    public IEnumerable<float> L2R() {
        StartCoroutine(PopcornPatternLeftRight());
        yield return 7f;
    }
    
    //Time: 4.9*2 = 14
    IEnumerator PopcornPatternRLLR() {
        StartCoroutine(PopcornPatternRightLeft());
        yield return new WaitForSeconds(7f);
        StartCoroutine(PopcornPatternLeftRight());
    }

    //Time = 5*popcorntime + 2.5
    public IEnumerator PopcornPatternX(float a) {
        StartCoroutine(SpawnPopcorn(5,0,a));
        StartCoroutine(SpawnPopcorn(5,2,-a));
        yield return new WaitForSeconds(0);
    }

    IEnumerator SpawnPopcorn(int n, int lane, float angle) {
        for (int i = 0; i<n; i++) {
            GameObject g =  Instantiate(Popcorn);
            g.SetActive(true);
            g.transform.position = lanes[lane];
            g.transform.Rotate(0,0,angle);
            g.GetComponent<PopcornGroup>().dir = Quaternion.Euler(0,0,angle)*Vector3.down;
            g.GetComponent<EnemyAI>().TriggerAI(true);
            strayEnemies.Add(g);
            yield return new WaitForSeconds(popcornTime);
        }
    }

    public IEnumerator PopcornPatternAngleInwards() {
        float a = 15;
        for (int i = 0; i < 4; i++) {
            StartCoroutine(PopcornPatternX(a));
            a += 10;
            yield return new WaitForSeconds(2.5f+popcornTime*5);
        }
    }
    public IEnumerable<float> AI() {
        StartCoroutine(PopcornPatternAngleInwards());
        yield return (2.5f+popcornTime*5)*4;
    }

}
