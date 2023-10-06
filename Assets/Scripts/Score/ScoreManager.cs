using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public GameObject ScoreText;
    public GameObject MultiText;

    [SerializeField]
    public int score = 0;
    public int scoreOnlyGains = 0;

    private int maxScore = 0;

    public bool canDeplete = true;

    //private float Timer = 0;

    public int GetScore() {
        return score;
    }

    Coroutine x;
    Coroutine y;
    public void Increment(int i) {
        StopCoroutine(x);
        x = StartCoroutine(SetDecayFalseForSeconds(2.5f));
        score += i;
        scoreOnlyGains += i;
    }

    public void Decrement(int i) {
        if (canDeplete) {
            if (score > i) score -= i;
            else score = 0;
        }
    }
    
    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ScoreText.SetActive(true);
        x = StartCoroutine(SetDecayFalseForSeconds(5f));
    }

    // Update is called once per frame
    void Update()
    {
        if (score >= maxScore) maxScore = score;
        if (decayCoolDownComplete && startDecay) y = StartCoroutine(Decay());
        
    }
    
    private void LateUpdate() {
        ScoreText.GetComponent<TMP_Text>().SetText(scoreOnlyGains.ToString());
        MultiText.GetComponent<TMP_Text>().SetText(GetScaling().ToString() + "x");
    }

    private bool decayCoolDownComplete = true;
    private int decrementAmnt = 10;
    IEnumerator Decay() {
        float timescale = 0.75f;

        decayCoolDownComplete = false;
        yield return new WaitForSeconds(timescale);
        Decrement(decrementAmnt);
        decayCoolDownComplete = true;
    }

    bool startDecay = false;
    IEnumerator SetDecayFalseForSeconds(float f) {
        startDecay = false;
        if (y != null) StopCoroutine(y);
        yield return new WaitForSeconds(f);
        startDecay = true;
        decayCoolDownComplete = true;
        decrementAmnt = Mathf.RoundToInt(score*0.1f);
    }

    public float GetScaling() {
        float s = score / 5000f;

        if (s < 1f) s = 1;

        if (s > 2.5f) s = 2.5f;

        return (float)Math.Truncate(s*100) / 100;
    }
    
}
