using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class slimeState : StateMachineBehaviour
{
    Transform target;
    Transform borderCheck;
    LayerMask groundMask;
    float health;

    float delayBetweenAttacks = 1f;
    float delayTimer = 0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        borderCheck = animator.GetComponent<Enemy>().borderCheck;
        groundMask = animator.GetComponent<Enemy>().groundMask;

        health = animator.GetComponent<Health>().health;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Physics2D.Raycast(borderCheck.position, Vector2.down, 2f, groundMask) == false)
            return;
        float distance = Vector2.Distance(target.position, animator.transform.position);

        if (animator.GetBool("isOnDelay") == true)
        {
            delayTimer += Time.deltaTime;
        }
        if (distance < 7 && health > 0)
        {
            if (distance > 2)
                animator.SetBool("isChasing", true);
            else if (!animator.GetBool("isOnDelay"))
                animator.SetBool("isClosed", true);
        }

        if (delayTimer > delayBetweenAttacks)
        {
            animator.SetBool("isOnDelay", false); 
            delayTimer = 0f;
        }

        if (health <= 0)
        {
            animator.SetBool("isDead", true);
            animator.SetBool("isChasing", false);
            animator.SetBool("isClosed", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
