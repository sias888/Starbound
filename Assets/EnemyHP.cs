using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : Enemy
{
    public float MaxHP = 100;
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

        transform.gameObject.GetComponentInChildren<SpriteRenderer>().material.shader = PaintWhite;
            
        // show a white flash for a little moment
        yield return new WaitForSeconds(0.05f);
    
        //put again the shader it had before 
        transform.gameObject.GetComponentInChildren<SpriteRenderer>().material.shader = CurShader;
    }


    void Die() {
        //animator.Play("EnemyDeath");
        //Destroy(transform.gameObject);
        gameObject.GetComponent<EnemyAI>().OnDeath();
    }

}
