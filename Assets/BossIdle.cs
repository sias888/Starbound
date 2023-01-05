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

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResetAllAnimatorTriggers(animator);
        canExit = false;
        timer = Random.Range(minTime, maxTime);

        nextState = Random.Range(0,20);
        //TO BE REMOVED
        //nextState = 0;

        attackType = Random.Range(0,10);
        //TO BE REMOVED
        //attackType = 2;

        boss = animator.GetComponent<BossAI>();

        if (attackType <= 4) {
            boss.LightAttackBullet();
            //nextState = 12;
        } else if (attackType <= 10) {
                boss.LightAttackBeam();
                //nextState = 12;
            } else if (attackType <= 10) {
                    canExit = true;
                }
        

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0 && canExit) {

            if (nextState < 9) {
                animator.SetTrigger("Attack");
            } else if (nextState < 18) {
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