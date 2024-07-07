using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeClosed : StateMachineBehaviour
{
    Transform target;
    float distance;
    bool isAttacking;
    float health;

    bool isActiveAttackArea = true;
    float timer = 0f;
    float attackTime = 0f;

    GameObject attackArea;
    Animator animator;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        animator = animator.GetComponent<Enemy>().animator;
        health = animator.GetComponent<Health>().health;
        attackArea = animator.GetComponent<Enemy>().attackArea;
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "slimer")
                attackTime = clip.length * 2;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distance = Vector2.Distance(target.position, animator.transform.position);

        timer += Time.deltaTime;
        Debug.Log(timer);
        if (timer >= attackTime * 0.66 && timer <= attackTime * 0.66 + 0.05f)
        {
            isActiveAttackArea = true;
        }
        if (timer > attackTime * 0.66 + 0.05f || timer < attackTime * 0.66)
            isActiveAttackArea = false;

        //Debug.Log("bool = " + isActiveAttackArea + " timer = " + timer);
        attackArea.SetActive(isActiveAttackArea);

        if (timer > attackTime)
        {
            timer = 0f;
            animator.SetBool("isClosed", false);
            animator.SetBool("isChasing", false);
            animator.SetBool("isOnDelay", true);
        }
        else if (((distance > 2 && !isAttacking) || isActiveAttackArea) && health > 0)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
            {
                animator.SetBool("isClosed", false);
                animator.SetBool("isChasing", true);
            }
        }

        if (health <= 0)
        {
            animator.SetBool("isDead", true);
            animator.SetBool("isChasing", false);
            animator.SetBool("isClosed", false);
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
