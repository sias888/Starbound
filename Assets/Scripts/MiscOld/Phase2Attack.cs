using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2Attack : StateMachineBehaviour
{

    private int heavyAttackType;
    private int lightAttackType;

    private int canExit;

    private BossAI boss;

    public static Phase2Attack instance;

    void Awake() {
        instance = this;
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BossIdle.instance.ResetAllAnimatorTriggers(animator);

        boss = animator.GetComponent<BossAI>();
        canExit = 0;
        heavyAttackType = Random.Range(0,3);
        lightAttackType = Random.Range(0,3);

        //attackType = 1;

        //TO BE REMOVED
        //Debug.Log(attackType);

        if (heavyAttackType == 0) boss.Spin();

        if (heavyAttackType == 1) boss.Circle();

        if (heavyAttackType == 2) boss.TwoBeam();

        boss.LightAttackBullet();

        //if (lightAttackType == 1) boss.LightAttackBeam();

        //if (lightAttackType == 2) boss.LightBeamBlast();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (canExit == 2) {
            animator.SetTrigger("AttackCD");
        }
    }

    public void CanExit(bool b) {
        if (b == true) canExit++;
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
