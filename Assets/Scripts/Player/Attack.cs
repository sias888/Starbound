using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    //Further ideas:
    // play animation on ship during anticipation frames of first swipe KINDA DONE
    // make second swipe time-limited OR indicate that second swipe is available DONE
    // add input buffering NOT REALLY NEEDED ANYMORE
    // berserker mode? (much smaller antcipation time, seocnd hit comes automatically)
        //Less anticipation frames, second hit is automatic, shoot is now a continuous red beam, no dodge

    public GameObject AttackPrefab;
    public GameObject AttackFollowPrefab;
    GameObject AttackPrefabClone;
    public Transform AttackPoint;
    public float StamFillVal = 5f;
    public float AttackDmgVal = 25f;

    //public Sprite originalSprite;
    //public Sprite changedSprite;

    private SpriteRenderer mySprite;

    public static Attack instance;

    enum AttackState {
        Ready,
        Attacking,
        ReadyFollow,
        Cooldown,
    }

    public float anticipation = 0.25f;
    public float attackTime = (1/24)*10f;
    public float attackPostTime = 0.2f;
    //public float followTime = (1/24)*5f;
    //public float followOpening = 0.5f;
    //public float cooldown = 1f;

    private AttackState attackState;

    //public float attackRange = 5f;
    public LayerMask enemyLayers;

    // Start is called before the first frame update

    void Awake() {
        instance = this;
    }

    void Start()
    {
        attackState = AttackState.Ready;
        mySprite = gameObject.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerScript.instance.isDead) return;

        if (PlayerInput.instance.getMeleePressed() && HealthScript.instance.isHealing == false && !PauseControls.isPaused) {
            if (attackState == AttackState.Ready) AttackMethod();
            if (attackState == AttackState.ReadyFollow) AttackFollow();
            PlayerInput.instance.setMeleePressed(false);
        }

        /*
        if ((Input.GetKeyDown(KeyCode.Quote) || Input.GetButtonDown("Melee")) && attackState == AttackState.Ready) {
            AttackMethod();
        }

        if ((Input.GetKeyDown(KeyCode.Quote) || Input.GetButtonDown("Melee")) && attackState == AttackState.ReadyFollow) {
            AttackFollow();
        }*/
    }

    void AttackMethod() {
        attackState = AttackState.Attacking;
        StartCoroutine(AttackCooldown());
    }

    IEnumerator AttackCooldown() {

        yield return new WaitForSeconds(anticipation);

        AttackPrefabClone = Instantiate(AttackPrefab, AttackPoint);
        PlayerMeleeAudio.instance.PlayClip();
        attackState = AttackState.Attacking;
        yield return new WaitForSeconds(attackTime);

        yield return new WaitForSeconds(attackPostTime);
        attackState = AttackState.ReadyFollow;
        //mySprite.sprite = originalSprite;
    }

    void AttackFollow() {
        attackState = AttackState.Attacking;
        StartCoroutine(AttackFollowCooldown());
    }

    IEnumerator AttackFollowCooldown() {
        //yield return new WaitForSeconds(anticipation);
        AttackPrefabClone = Instantiate(AttackFollowPrefab, AttackPoint);
        PlayerMeleeAudio.instance.PlayClip();
        attackState = AttackState.Attacking;
        yield return new WaitForSeconds(attackTime);
        
        yield return new WaitForSeconds(attackPostTime);
        attackState = AttackState.Ready;
        //mySprite.sprite = originalSprite;
    }

    public void FillStam() {
        EnergyBar.instance.fillEnergy(StamFillVal);
    }

    public void FillStam(float ratio) {
        EnergyBar.instance.fillEnergy(StamFillVal*ratio);
    }

    /*
    public void CreateHitbox() {
        Debug.Log("Hitbox Created!");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayers);
        List<Collider2D> enemiesAlreadyHit = new List<Collider2D>();

        foreach(Collider2D enemy in hitEnemies) {
            if (!enemiesAlreadyHit.Contains(enemy)) {
                enemy.GetComponent<Enemy>().TakeDamage(20f);
                enemiesAlreadyHit.Add(enemy);
            }
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
    }

    public void DestroyHitbox() {
        Debug.Log("Hitbox Destroyed!");

    }
    */

}
