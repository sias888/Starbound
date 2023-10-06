using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScoreScreen : MonoBehaviour
{

    public GameObject EventSystemGameObject;
    public GameObject RetryButton;
    // Start is called before the first frame update
    void Start()
    {
        EventSystemGameObject.GetComponent<EventSystem>().SetSelectedGameObject(RetryButton);

        points = transform.GetChild(0).gameObject;
        performance = transform.GetChild(1).gameObject;
        multiplier = transform.GetChild(2).gameObject;
        total = transform.GetChild(3).gameObject;

        

        points.GetComponent<TMP_Text>().SetText(ScoreManager.instance.scoreOnlyGains.ToString());
        performance.GetComponent<TMP_Text>().SetText(ScoreManager.instance.score.ToString());
        multiplier.GetComponent<TMP_Text>().SetText(ScoreManager.instance.GetScaling().ToString());

        total.GetComponent<TMP_Text>().SetText(((int)(ScoreManager.instance.GetScaling()*ScoreManager.instance.score*10
                                                 + ScoreManager.instance.scoreOnlyGains)).ToString());
    }

    

    GameObject points;
    GameObject performance;
    
    GameObject multiplier;

    GameObject total;

    // Update is called once per frame
    void Update()
    {
        
    }
}
