using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHP : Enemy
{
    public BossHPBar HealthBar;

    float MaxHP = 5000;
    private float currentHP;

    private Shake shakeScreen;

    private Animator anim;

    public Shader PaintWhite;
    Shader CurShader;

    void Awake()
    {
        currentHP = MaxHP;
        anim = gameObject.GetComponent<Animator>();
        shakeScreen = GameObject.FindGameObjectWithTag("ShakeScreen").GetComponent<Shake>();
        CurShader = GetComponentInChildren<SpriteRenderer>().material.shader;
    }

    public override void TakeDamage(float amnt) {
        currentHP -= amnt;
        HealthBar.LoseHP(amnt);

        //anim.SetTrigger("Hurt");

        if (amnt < 10) {
            shakeScreen.SmallShake();
        } else {
            shakeScreen.MedShake();
        }

        StartCoroutine(Hurt());

        if (currentHP <= 0) {
            Die();
        }

    }


    IEnumerator Hurt() {
        //transform.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        //yield return new WaitForSeconds(0.20f);
        //transform.gameObject.GetComponent<SpriteRenderer>().color = Color.white;

        //Shader Cur = transform.gameObject.GetComponent<SpriteRenderer>();


        transform.gameObject.GetComponentInChildren<SpriteRenderer>().material.shader = PaintWhite;
            
        // show a white flash for a little moment
        yield return new WaitForSeconds(0.05f);
    
        //put again the shader it had before 
        transform.gameObject.GetComponentInChildren<SpriteRenderer>().material.shader = CurShader;
    }


    void Die() {
        //animator.Play("EnemyDeath");
        transform.gameObject.GetComponent<BossAI>().OnDeath();
    }

}
