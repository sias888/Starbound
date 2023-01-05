using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTP : StateMachineBehaviour
{

    private int nextLocation;

    private Vector3 currPos;

    private Vector3[] positions = new Vector3[] {new Vector3(-6, 3.5f, 1), new Vector3(6, 3.5f, 1), new Vector3(0, 3, 1)};
    private int currIndex;
    private int indexA;
    private int indexB;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        nextLocation = Random.Range(0,9);

        currPos = animator.transform.position;

        currIndex = System.Array.IndexOf(positions, currPos);

        if (currIndex == 1) {
            indexA = 2;
            indexB = 0;
        }

        if (currIndex == 0) {
            indexA = 2;
            indexB = 1;
        }

        if (currIndex == 2) {
            indexA = 1;
            indexB = 0;
        }

        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (0 == nextLocation || nextLocation == 1) {
            animator.transform.position = currPos;
        } else if ((nextLocation % 2) == 0) {
            animator.transform.position = positions[indexA];
        } else {
            animator.transform.position = positions[indexB];
        }
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
