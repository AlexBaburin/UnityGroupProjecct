using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeClosed : StateMachineBehaviour
{
    Transform target;
    float distance;
    bool isAttacking;

    Animator animator;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        animator = animator.GetComponent<Enemy>().animator;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distance = Vector2.Distance(target.position, animator.transform.position);
        Debug.Log($"dis = {distance}, isAttacking = {isAttacking}");
        if (distance > 2 && !isAttacking)
        {
            Debug.Log("IT HAS GONE");
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                animator.SetBool("isClosed", false);
                animator.SetBool("isChasing", true);
            }
        }
    }
    bool AnimatorIsPlaying(Animator animator, string stateName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isAttacking = AnimatorIsPlaying(animator, "slimer");
    }
}
