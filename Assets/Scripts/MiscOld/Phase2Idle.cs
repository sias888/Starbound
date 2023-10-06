using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2Idle : StateMachineBehaviour
{

    private float timer;

    public float minTime;
    public float maxTime;

    private int nextState;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BossIdle.instance.ResetAllAnimatorTriggers(animator);

        timer = Random.Range(minTime, maxTime);

        nextState = Random.Range(0,20);

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0) {

            if (nextState <= 14) {
                animator.SetTrigger("Attack");
            } else if (nextState <= 20) {
                animator.SetTrigger("TP");
            }

        } else {
            timer -= Time.deltaTime;
        }    
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
