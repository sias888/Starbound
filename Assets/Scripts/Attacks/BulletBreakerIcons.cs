using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBreakerIcons : MonoBehaviour
{
    public static BulletBreakerIcons instance;

    private void Awake() {
        instance = this;
    }
    public void BulletBreak(int i) {
        if (i >= 0)
            transform.GetChild(i).gameObject.SetActive(false);
    }
}
