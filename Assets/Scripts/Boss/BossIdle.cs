using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdle : StateMachineBehaviour
{
    private float timer;

    public float minTime;
    public float maxTime;

    private int nextState;

    private int attackType;

    private bool canExit;

    private BossAI boss;

    public static BossIdle instance;

    void Awake() {
        instance = this;
    }

    public int Phase = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResetAllAnimatorTriggers(animator);
        canExit = false;
        timer = Random.Range(minTime, maxTime);

        if (animator.gameObject.GetComponent<BossHP>().GetHPPercent() < 0.85)
            Phase = 1;
        if (animator.gameObject.GetComponent<BossHP>().GetHPPercent() < 0.7)
            Phase = 2;
        if (animator.gameObject.GetComponent<BossHP>().GetHPPercent() < 0.3)
            Phase = 3;

        if (ScoreManager.instance.GetScaling() > 2f && Phase <= 1) Phase = 2;

        if (Phase > 0)
            nextState = Random.Range(0,20);
        else
            nextState = Random.Range(12,25);

        if (Phase >= 2) {
            animator.gameObject.GetComponent<BossAI>().ShootDrills();
        }

        if (Phase >= 3) {
            Debug.Log(Phase);
            animator.gameObject.GetComponent<BossAI>().startFirePopcorn = true;
        }
        //TO BE REMOVED
        //nextState = 0;

        attackType = Random.Range(0,19);
        //TO BE REMOVED

        boss = animator.GetComponent<BossAI>();

        if (attackType <= 3) {
            boss.LightAttackBullet();
            //nextState = 12;
        } else if (attackType <= 6) {
                boss.LightAttackBeam();
                //nextState = 12;
            } else if (attackType <= 9) {
                    boss.LightBeamBlast();
                } else canExit = true;
        

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0 && canExit) {

            if (nextState < 12) {
                animator.SetTrigger("Attack");
            } else if (nextState < 17) {
                animator.gameObject.GetComponent<BossAI>().TriggerDrills(false);
                animator.SetTrigger("TP");
            } else {
                animator.SetTrigger("Idle");
            }

        } else {
            timer -= Time.deltaTime;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    public void CanExit(bool b) {
        canExit = b;
    }

    public void ResetAllAnimatorTriggers(Animator animator)
    {
        foreach (var trigger in animator.parameters)
        {
            if (trigger.type == AnimatorControllerParameterType.Trigger)
            {
                animator.ResetTrigger(trigger.name);
            }
        }
    }
     



}