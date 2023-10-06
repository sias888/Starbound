using System.Collections;
using UnityEngine;

public class BossHP : Enemy
{
    public BossHPBar HealthBar;

    public float MaxHP;
    private float currentHP;

    public float GetCurrentHP() {
        return currentHP;
    }

    public float GetHPPercent() {
        return currentHP/MaxHP;
    }

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
        HealthBar.MaxHP = MaxHP;
    }

    public override void TakeDamage(float amnt) {
        currentHP -= amnt;
        HealthBar.LoseHP(amnt);

        //if (currentHP <= MaxHP/2) anim.SetTrigger("Phase2");

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
        yield return new WaitForSeconds(0.01f);
    
        //put again the shader it had before 
        transform.gameObject.GetComponentInChildren<SpriteRenderer>().material.shader = CurShader;
    }


    void Die() {
        //animator.Play("EnemyDeath");
        //transform.gameObject.GetComponent<BossAI>().OnDeath();
        anim.SetTrigger("Death");
        BossHPBar.instance.Die();
    }

}
