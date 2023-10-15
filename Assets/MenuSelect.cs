using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelect : PlayerAudio
{
    // Start is called before the first frame update
    public static MenuSelect instance;

    private void Awake() {
        instance = this;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

}