using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : MonoBehaviour
{
    public Slider bossHP;

    public GameObject boss;

    public float MaxHP = 6000;
    float currentHP;

    public static BossHPBar instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        MaxHP = boss.GetComponent<BossHP>().MaxHP;
    }

    void Start() {
        currentHP = MaxHP;
        bossHP.maxValue = MaxHP;
        bossHP.value = currentHP;
    }

    public void LoseHP(float a) {
        currentHP -= a;
        bossHP.value = currentHP;

        if (currentHP <= 0) {
            bossHP.gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }

    void Update() {
        if (currentHP <= 0) {
            bossHP.gameObject.SetActive(false);
        } 
    }

    public void Die() {
        gameObject.SetActive(false);
        bossHP.gameObject.SetActive(false);
    }
}
