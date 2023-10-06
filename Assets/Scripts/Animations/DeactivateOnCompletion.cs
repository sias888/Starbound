using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOnCompletion : StateMachineBehaviour
{
    //float t;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*//Invoke("Destroy", stateInfo.length);
        Debug.Log("hi");
        t = stateInfo.length;*/
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*
        while (true) {
            if (t <= 0) {
                animator.gameObject.transform.SetParent(null, false);
                animator.gameObject.SetActive(false);
                break;
            } else {
                t -= Time.deltaTime;
            }
        } */   
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("hi");
        animator.gameObject.transform.SetParent(null, false);
        animator.gameObject.SetActive(false);
    }

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
