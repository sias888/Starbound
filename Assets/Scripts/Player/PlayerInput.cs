using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    //Will receive all player inputs and broadcast to relevant parties.
    public static PlayerInput instance;

    public InputActionAsset inputActions;

    private Vector2 moveVector;
    public  Vector2 getMoveVector() {
        return moveVector;
    }

    private bool dodgePressed = false;
    public bool getDodgePressed() {
        bool b = dodgePressed;
        dodgePressed = false;
        return b;
    }

    public void SetDodgePressed(bool b) {
        dodgePressed = b;
    }

    private bool meleePressed = false;
    public  bool getMeleePressed() {
        return meleePressed;
    }
    public void setMeleePressed(bool b) {
        meleePressed = b;
    }

    private bool firePressed;
    public  bool getFirePressed() {
        return firePressed;
    }

    // 1 = started
    // 2 = performed
    // 0 = canceled
    private int healPressed;
    public  int getHealPressed() {
        return healPressed;
    }

    void Awake() {
        instance = this;
        PlayerInput.instance.EnableDodge(true);
    }

    public void EnableDodge(bool b) {
        if (b) {
            inputActions.FindAction("Dodge").Enable();
            return;
        }
        inputActions.FindAction("Dodge").Disable();
    }


    public void Dodge(InputAction.CallbackContext context) {
        if (context.started && dodgePressed == false && !PauseControls.isPaused) {
            dodgePressed = true;
        }

        if (context.performed) {
            dodgePressed = false;
        }
    }

    public void Melee(InputAction.CallbackContext context) {
        if (context.started && meleePressed == false) {
            //PlayerScript.instance.SetDodgePressed(true);
            meleePressed = true;
        }
        if (context.canceled) {
          //PlayerScript.instance.SetDodgePressed(false);
            meleePressed = false;
        }
    }

    public void Fire(InputAction.CallbackContext context) {
        if (context.performed) {
            //PlayerScript.instance.SetDodgePressed(true);
            firePressed = true;
        }

        if (context.canceled) {
          //PlayerScript.instance.SetDodgePressed(false);
            firePressed = false;
        }
    }

    public void Move(InputAction.CallbackContext context) {
        moveVector = context.ReadValue<Vector2>();
    }

    public void Heal(InputAction.CallbackContext context) {
        if (context.started)   healPressed = 1;
        if (context.performed) healPressed = 2;
        if (context.canceled)  healPressed = 0;
    }

    public void PreHeal(InputAction.CallbackContext context) {
        //Debug.Log(context.phase);
        if (context.started) Debug.Log("Start PreHeal Animation");

        if (context.performed) Debug.Log("Start Heal Animation");

        if (context.canceled) Debug.Log("Finish Preheal and (if exists) Heal Animation");
    }
}
