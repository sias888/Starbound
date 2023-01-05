using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class HealthScript : MonoBehaviour
{
    public static HealthScript instance;

    public GameObject HealPrefab;
    private GameObject h;

    Animator animator;
    Rigidbody2D rigidBody;
    public float maxHealth = 100;
    private float currentHealth;

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

    void Awake()
    {
        instance = this;

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

    void Update() {

        if (Input.GetButtonDown("Heal")) {
            h = Instantiate(HealPrefab, transform);
            isHealing = true;
        }

        
        if (Input.GetButton("Heal")) {
            if (EnergyBar.instance.GetCur() > 0.2f) {
                EnergyBar.instance.useEnergy(0.2f);
                Heal(0.2f*(1/3f));
            }
        }

        if (Input.GetButtonUp("Heal")) {
            Destroy(h);
            isHealing = false;
        }

    }

    public void TakeDamage(float damage) {

        if (canTakeDamage) {

            shakeScreen.LargeShake();

            currentHealth -= damage;
            HealthBar.instance.LoseHP(damage);

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

        sr.sprite = whiteSprite;
        yield return new WaitForSeconds(0.1f);
        sr.sprite = defaultSprite;
        yield return new WaitForSeconds(0.05f);
        sr.sprite = whiteSprite;
        yield return new WaitForSeconds(0.1f);
        sr.sprite = defaultSprite;
        yield return new WaitForSeconds(0.05f);
        sr.sprite = whiteSprite;
        yield return new WaitForSeconds(0.1f);
        sr.sprite = defaultSprite;
        yield return new WaitForSeconds(0.05f);
        sr.sprite = whiteSprite;
        yield return new WaitForSeconds(0.1f);
        sr.sprite = defaultSprite;
        yield return new WaitForSeconds(graceTime - 4*0.1f - 3*0.05f);

        canTakeDamage = true;
    }

    void Die() {
        Debug.Log("You died!");
        transform.GetComponent<PlayerScript>().isDead = true;
    }
}
