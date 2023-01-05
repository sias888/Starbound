using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : MonoBehaviour
{
    public Slider bossHP;

    float MaxHP = 5000;
    float currentHP;

    public static BossHPBar instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
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
        }
    }
}
