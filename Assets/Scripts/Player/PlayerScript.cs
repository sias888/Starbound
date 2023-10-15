using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;


// Player functionality:
// 1) Move. All cardinal directions. Must be smooth, responsive. DONE
// 2) Shoot. Primary means of attack. Can be spammable, intended to be the "hold down and dont think much" option.
//    Has a meter. Shooting will reduce meter. At empty meter, must wait until meter is halfway full. Stamina management. YELLOW
// 3) Dodge. Has i-frames. Small unidirectional dodge. Shoots player in last moved direction a set distance.
//    Cannot move during a dodge. Has startup frames. Can shoot while dodging. DESATURATED GREEN.
// 4) Melee. Secondary means of attack. Lower range, higher damage. Has startup and endlag. Hitbox is > 180 but < 270 degrees.
//    Sweet spot in front 90 deg does extra damage. YELLOW. Sweet spot: ORANGE.
// I need a new ship model. Simple--colour should be the main language DONE

public class PlayerScript : MonoBehaviour {

    public GameObject dodgePrefab;

    public GameObject BulletBreaker;

    private int BBcount = 3;

    bool canBulletBreak = true;

    public static PlayerScript instance;

    enum State {
        Move,
        Dodge,
        Cooldown,
        Knockback,
    }
    State PlayerState;

    public bool isDead;

    private Vector2 velocity = new Vector2(0,0);

    Rigidbody2D myRigidbody;
    TrailRenderer trail;

    CircleCollider2D[] hurtboxes;

    //[SerializeField]
    //private float MOVESPEED = 6.25f;
    [SerializeField]
    private float DODGESPEED = 20.5f;
    [SerializeField]
    private float DODGEDURATION = 9f;
    [SerializeField]
    private float DODGECOOLDOWN = 150f;

    public Vector2 Value;
    public float timeFromZeroToMax = 0.001f;
    public float maxSpeed = 5.5f;
    Vector2 moveTowards = new Vector2(0,0);

    public InputActionAsset inputActions;

    void Awake() {
        instance = this;
        
    }

    // Start is called before the first frame update
    void Start() {
        trail = transform.GetChild(0).GetComponent<TrailRenderer>();
        
        myRigidbody = GetComponent<Rigidbody2D>();
        PlayerState = State.Move;

        //screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));

        hurtboxes = transform.GetChild(3).GetComponents<CircleCollider2D>();

        isDead = false;
    }

    // Update is called once per frame
    void Update() {

        //If able to move, reset velocity to zero
        if (PlayerState == State.Move || PlayerState == State.Cooldown) {
            velocity = Vector2.zero;
            HandleInputNew();
        }

        if (isDead) {
            velocity = Vector2.zero;
        }

    }

    void FixedUpdate() {
        if (PlayerState == State.Move || PlayerState == State.Cooldown)
            MoveNew();
    }

    void HandleInputNew() {
        if(isDead) {
            return;
        }

        if (PauseControls.isPaused) {
            return;
        }

        float changeRatePerSecond = Time.deltaTime/timeFromZeroToMax;
        moveTowards = new Vector2(0,0);
        moveTowards = PlayerInput.instance.getMoveVector();
        moveTowards.Normalize();

        if (!HealthScript.instance.isHealing) {
            Value.x = Mathf.MoveTowards(Value.x, moveTowards.x*maxSpeed, changeRatePerSecond);
            Value.y = Mathf.MoveTowards(Value.y, moveTowards.y*maxSpeed, changeRatePerSecond);
        } else {
            Value.x = Mathf.MoveTowards(Value.x, moveTowards.x*maxSpeed/2, changeRatePerSecond);
            Value.y = Mathf.MoveTowards(Value.y, moveTowards.y*maxSpeed/2, changeRatePerSecond);
        }

        
        if (PlayerInput.instance.getDodgePressed() && PlayerState == State.Move && HealthScript.instance.isHealing == false) {
            if (moveTowards == Vector2.zero)
                moveTowards = Vector2.up;
            Dodge();
            PlayerInput.instance.SetDodgePressed(false);
        }

        
        
        if (HealthScript.instance.isHealing && inputActions.FindAction("Dodge").WasPressedThisFrame()) {
            if (canBulletBreak && BBcount > 0) {
                GameObject g = Instantiate(BulletBreaker);
                g.SetActive(true);
                BBcount--;
                BulletBreakerIcons.instance.BulletBreak(BBcount);
                canBulletBreak = false;
                Invoke("SetBBTrue", 2.5f);
            }
        }
        
    }

    void SetBBTrue() {
        canBulletBreak = true;
    }

    /*void Move() {

        //set transform.position from Vector3 to Vector2
        //Vector2 v = transform.position;
        //myRigidbody.MovePosition(v + velocity * Time.fixedDeltaTime * MOVESPEED);

        myRigidbody.velocity = velocity * MOVESPEED;

    }*/


    void MoveNew() {
        myRigidbody.velocity = Value;
    }


    void Dodge() {
        StartCoroutine(DodgeRoutine());
    }

    IEnumerator DodgeRoutine() {
        PlayerState = State.Dodge;

        //Start animation
        Instantiate(dodgePrefab, transform);
        PlayerDodgeAudio.instance.PlayClip();
        
        trail.emitting = true;

        //Start dodge
        myRigidbody.velocity = moveTowards * DODGESPEED;

        foreach(CircleCollider2D col in hurtboxes) {
            col.enabled = false;
        }

        //Allow animation to occur
        yield return new WaitForSeconds(DODGEDURATION);

        foreach(CircleCollider2D col in hurtboxes) {
            col.enabled = true;
        }


        PlayerState = State.Move;
        myRigidbody.velocity = Vector2.zero;
        PlayerState = State.Cooldown;
        //Cooldown afterwards
        yield return new WaitForSeconds(DODGECOOLDOWN);
        trail.emitting = false;
        PlayerState = State.Move;
    }

    public void SetKnockback(bool b) {
        State currentState = PlayerState;
        if (b == true) {
            PlayerState = State.Knockback;
            foreach(CircleCollider2D col in hurtboxes) {
                col.enabled = false;
            }
        } else {
            PlayerState = State.Move;
            foreach(CircleCollider2D col in hurtboxes) {
                col.enabled = true;
            }
        }
    }

}
