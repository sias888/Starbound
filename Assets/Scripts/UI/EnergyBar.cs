using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider energyBar;

    private float maxEnergy = 200f;
    private float currentEnergy;

    public static EnergyBar instance;

    //private bool canFill = true;

    void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentEnergy = maxEnergy;
        energyBar.maxValue = maxEnergy;
        energyBar.value = currentEnergy;
        //canFill = true;
    }

    public void useEnergy(float amount) {
        if (currentEnergy >= 0 + amount) {
            currentEnergy -= amount;
            energyBar.value = currentEnergy;
        }
    }

    public void fillEnergy(float amount) {

        if (currentEnergy < maxEnergy - amount) {
                currentEnergy += amount;
                energyBar.value = currentEnergy;
            } else if (currentEnergy < maxEnergy) {
                currentEnergy = maxEnergy;
                energyBar.value = currentEnergy;
        }
    }

    public bool canFire(float amount) {
        if (currentEnergy > 0 + amount)
            return true;
        return false;
    }

    public float GetCur() {
        return currentEnergy;
    }

}
