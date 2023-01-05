using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;

    float MaxHP = 100f;
    float currentHP;

    public static HealthBar instance;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHP = MaxHP;
        healthBar.maxValue = MaxHP;
        healthBar.value = currentHP;
    }

    public void LoseHP(float amount) {
        currentHP -= amount;
        if (currentHP < 0) currentHP = 0;
        healthBar.value = currentHP;
    }

    public void GainHP( float amount) {
        currentHP += amount;
        if (currentHP > MaxHP) currentHP = MaxHP;
        healthBar.value = currentHP;
    } 

}
