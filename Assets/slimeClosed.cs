using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeClosed : StateMachineBehaviour
{
    Transform target;
    float distance;
    bool isAttacking;

    bool isActiveAttackArea = true, isAttacked = false;
    float timer = 0f;
    float frameOfAttack = 0.3f;

    GameObject attackArea;
    Animator animator;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        animator = animator.GetComponent<Enemy>().animator;
        attackArea = animator.GetComponent<Enemy>().attackArea;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distance = Vector2.Distance(target.position, animator.transform.position);

        timer += Time.deltaTime;
        
        if (isAttacked)
        {
            timer = 0f;
            isAttacked = false;
            isActiveAttackArea = false;
        }
        if (timer >= 1)
        {
            isActiveAttackArea = true;
            isAttacked = true;
        }
        //Debug.Log("bool = " + isActiveAttackArea + " timer = " + timer);
        attackArea.SetActive(isActiveAttackArea);

        if ((distance > 2 && !isAttacking) || isActiveAttackArea)
        {
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
