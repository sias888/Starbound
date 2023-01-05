using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1EnemyController : MonoBehaviour
{

    public GameObject[] AEnemies;
    public GameObject[] BEnemies;
    public GameObject[] CEnemies;
    public GameObject[] DEnemies;

    private List<int> FreeEnemyList = new List<int>();

    private int wave;

    private bool startWave;

    /*void Awake() {
        FreeEnemyList.Add(AEnemies.Length);
        startWave = true;
        wave = 0;
    }*/

    void Start() {
        EventHandler.instance.onEnemyDeath += OnEnemyDeath;
        FreeEnemyList.Add(AEnemies.Length);
        startWave = true;
        wave = 0;
    }

    private void OnEnemyDeath(int i) {
        FreeEnemyList[i]++;
        Debug.Log("type: "+ i + " free:" + FreeEnemyList[i]);
        //Jump table to access wave-end func for given wave (to open startWave and increment wave);
        Wave0End();

    }

    void Update()
    {
        if (startWave) {
            switch (wave) {
                case 0:
                    StartCoroutine(Wave0());
                    break;
                case 1:
                    StartCoroutine(Wave1());
                    break;
                default:
                    break;
            }
        }
    }

    IEnumerator Wave0() {
        startWave = false;

        AEnemies[0].SetActive(true);
        AEnemies[1].SetActive(true);

        FreeEnemyList[0] -= 2;

        AEnemies[0].GetComponent<EnemyAI>().Move(new Vector2(-3, 2), 1f);
        AEnemies[1].GetComponent<EnemyAI>().Move(new Vector2( 3, 2), 1f);

        yield return new WaitForSeconds(2f);

        AEnemies[0].GetComponent<EnemyAI>().TriggerAI(true);
        AEnemies[1].GetComponent<EnemyAI>().TriggerAI(true);

        //yield return new WaitForSeconds(5f);
        //wave++;
        //startWave = true;
    }

    private void Wave0End() {
        Debug.Log("Entered the func");
        if (FreeEnemyList[0] == 2) {
            wave++;
            startWave = true;
            Debug.Log("Wave 0 ending...");
        }
    }

    IEnumerator Wave1() {
        startWave = false;
        Debug.Log("wave1 starting...");
        yield return new WaitForSeconds(5f);
        Debug.Log("wave1 ending...");
        wave++;
        startWave = true;
    }


    // Start is called before the first frame update
    //void Start()
    //{
        //enemies[0].GetComponent<EnemyAI>().TriggerAI(true);
        //enemies[1].GetComponent<EnemyAI>().TriggerAI(true);

        /*
        foreach(GameObject enemy in enemies) {
            enemy.GetComponent<EnemyAI>().TriggerAI(true);
        }
        */
    //}
}
