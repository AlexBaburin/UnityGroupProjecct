using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeState : StateMachineBehaviour
{
    Transform target;
    Transform borderCheck;
    LayerMask groundMask;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        borderCheck = animator.GetComponent<Enemy>().borderCheck;
        groundMask = animator.GetComponent<Enemy>().groundMask;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Physics2D.Raycast(borderCheck.position, Vector2.down, 2f, groundMask) == false)
            return;
        float distance = Vector2.Distance(target.position, animator.transform.position);
        if (distance < 7)
        {
            animator.SetBool("isChasing", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
