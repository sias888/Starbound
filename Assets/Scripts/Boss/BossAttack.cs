using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : StateMachineBehaviour
{
    private int attackType;

    private bool canExit;

    private BossAI boss;

    public static BossAttack instance;

    void Awake() {
        instance = this;
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BossIdle.instance.ResetAllAnimatorTriggers(animator);

        boss = animator.GetComponent<BossAI>();
        canExit = false;
        attackType = Random.Range(0,5);

        if (BossIdle.instance.Phase >= 3) {
            if (attackType < 2) boss.StartXBullets();
            else boss.LightAttackBullet();
        }

        //TO BE REMOVED
        //Debug.Log(attackType);

        if (attackType == 0) {
            boss.Spin();
        }

        if (attackType == 1) {
            boss.Circle();
        }

        if (attackType == 2) {
            boss.TwoBeam();
        }

        if (attackType == 3) {
            boss.BeamSpinBullets();
        }

        if (attackType == 4) {
            boss.BeamSpin();
        }

        if(animator.gameObject.GetComponent<BossHP>().GetHPPercent() < 0.5) {
            boss.ShootDrills();
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (canExit) {
            animator.SetTrigger("AttackCD");
        }
    }

    public void CanExit(bool b) {
        canExit = b;
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
