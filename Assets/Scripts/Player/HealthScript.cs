using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SocialPlatforms.Impl;

public class HealthScript : MonoBehaviour
{
    public static HealthScript instance;

    public GameObject HealPrefab;
    public GameObject PreHealPrefab;
    private GameObject h;
    private GameObject ph;

    Animator animator;
    Rigidbody2D rigidBody;
    public float maxHealth = 100;
    private float currentHealth;
    public float healScale = 0.33f;

    private Vector2 knockbackDirection;

    SpriteRenderer sr;

    public Sprite whiteSprite;
    Sprite defaultSprite;

    bool canTakeDamage;

    private Shake shakeScreen;

    public PostProcessVolume postProcessVolume;
    ChromaticAberration c;
    DepthOfField d;

    public float graceTime = 1f;

    public bool isHealing;

    public GameObject DeathScreen;

    public Shader PaintWhite;
    Shader CurShader;

    void Awake()
    {
        instance = this;

        CurShader = GetComponentInChildren<SpriteRenderer>().material.shader;

        currentHealth = maxHealth;
        animator = gameObject.GetComponent<Animator>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();

        sr = transform.GetChild(0).GetComponent<SpriteRenderer>();

        defaultSprite = sr.sprite;

        canTakeDamage = true;

        shakeScreen = GameObject.FindGameObjectWithTag("ShakeScreen").GetComponent<Shake>();

        postProcessVolume.weight = 0;
        postProcessVolume.profile.TryGetSettings(out c);
        postProcessVolume.profile.TryGetSettings(out d);

        isHealing = false;
    }

    public float HealSpeed = 0.1f;
    void Update() {

        if (PlayerScript.instance.isDead) return;
        
        //1 = button was pressed, 2 = button is being held (for x secs), 0 = let go of button
        int healPressed = PlayerInput.instance.getHealPressed();

        if (healPressed == 1 && !PauseControls.isPaused && PlayerScript.instance.enabled) {
            if (!ph) 
                ph = Instantiate(PreHealPrefab, transform.GetChild(5).transform);

            isHealing = true;
        }
        
        if (healPressed == 2 && !PauseControls.isPaused) {

            if (EnergyBar.instance.GetCur() > HealSpeed && currentHealth < maxHealth) {
                if (!h) h = Instantiate(HealPrefab, transform);
                HealAudio.instance.StartClip();
                EnergyBar.instance.useEnergy(HealSpeed);
                Heal(HealSpeed*0.5f);
            }
        }

        if (healPressed == 0 && !PauseControls.isPaused) {
            HealAudio.instance.StopClip();
            if (ph) Destroy(ph);
            if (h) Destroy(h);
            isHealing = false;
        }

    }

    public void TakeDamage(float damage) {

        if (canTakeDamage) {

            float scaling = 1.5f * 1/ScoreManager.instance.GetScaling();

            if (scaling > 1) scaling = 1;

            shakeScreen.LargeShake();

            currentHealth -= damage * scaling;
            HealthBar.instance.LoseHP(damage * scaling);
            ScoreManager.instance.Decrement(Mathf.RoundToInt(ScoreManager.instance.GetScore()*0.5f));

            PlayerDamaged.instance.PlayClip();
            StartCoroutine(AddScreenEffects());

            StartCoroutine(HurtAnimationRoutine());

            if (currentHealth <= 0) {
                Die();
            }

        }
    }

    public void Heal(float val) {
        currentHealth += val;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        HealthBar.instance.GainHP(val);

    }

    public float GetHealth() {
        return currentHealth;
    }

    IEnumerator AddScreenEffects() {
        postProcessVolume.weight = 1;
        yield return new WaitForSeconds(0.1f);

        postProcessVolume.weight = 0.8f;
        yield return new WaitForSeconds(0.1f);

        d.active = false;
        postProcessVolume.weight = 0.6f;
        yield return new WaitForSeconds(0.1f);

        postProcessVolume.weight = 0.4f;
        c.active = false;
        yield return new WaitForSeconds(0.1f);

        postProcessVolume.weight = 0;

        c.active = true;
        d.active = true;
    }

    IEnumerator HurtAnimationRoutine() {
        canTakeDamage = false;

        StartCoroutine(Hurt());

        yield return new WaitForSeconds(graceTime);
    
        canTakeDamage = true;
    }

    IEnumerator Hurt() {

        transform.gameObject.GetComponentInChildren<SpriteRenderer>().material.shader = PaintWhite;
            
        // show a white flash for a little moment
        yield return new WaitForSeconds(0.1f);
    
        //put again the shader it had before 
        transform.gameObject.GetComponentInChildren<SpriteRenderer>().material.shader = CurShader;

        yield return new WaitForSeconds(0.1f);

        transform.gameObject.GetComponentInChildren<SpriteRenderer>().material.shader = PaintWhite;
            
        // show a white flash for a little moment
        yield return new WaitForSeconds(0.1f);
    
        //put again the shader it had before 
        transform.gameObject.GetComponentInChildren<SpriteRenderer>().material.shader = CurShader;
    }

    void Die() {
        //Debug.Log("You died!");
        transform.GetComponent<PlayerScript>().isDead = true;
        Time.timeScale = 0f;
        DeathScreen.SetActive(true);
    }
}
