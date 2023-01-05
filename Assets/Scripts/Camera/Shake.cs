using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public Animator camAnim;

    public void SmallShake() {
        camAnim.SetTrigger("SmallShake");
    }

    public void MedShake() {
        camAnim.SetTrigger("MediumShake");
    }

    public void LargeShake() {
        camAnim.SetTrigger("LargeShake");
    }
}