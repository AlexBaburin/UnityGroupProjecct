using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chasingState : StateMachineBehaviour
{
    Rigidbody2D rb;
    Transform target;
    bool isPlayer = true, isGrounded = true;
    float distance;

    public float speed = 3;
    Transform borderCheck, groundCheck;
    LayerMask playerLayer, groundMask;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        borderCheck = animator.GetComponent<Enemy>().borderCheck;
        rb = animator.GetComponent<Enemy>().rb;
        playerLayer = animator.GetComponent<Enemy>().layerMask;
        groundMask = animator.GetComponent<Enemy>().groundMask;
        groundCheck = animator.GetComponent<Enemy>().groundCheck;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isPlayer = Physics2D.OverlapCircle(rb.position, 2f, playerLayer);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundMask);
        Vector2 newPos = new Vector2(target.position.x, animator.transform.position.y);
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, newPos, speed * Time.deltaTime);
        if ((Physics2D.Raycast(borderCheck.position, Vector2.left, 0.05f, groundMask) == true || Physics2D.Raycast(borderCheck.position, Vector2.right, 0.05f, groundMask) == true) && !isPlayer && isGrounded)
        {
            //Debug.Log("borderCheck == false");
            rb.velocity = new Vector2(rb.velocity.x, 7f);
        }
        if (Physics2D.Raycast(borderCheck.position, Vector2.down, 5f, groundMask) == false)
        {
            animator.SetBool("isChasing", false);
            animator.SetBool("isClosed", false);
        }

        distance = Vector2.Distance(target.position, animator.transform.position);
        if (distance < 2)
        {
            animator.SetBool("isClosed", true);
            animator.SetBool("isChasing", false);
        }
        //Debug.Log("isPlayer = " + isPlayer + " isGrounded = " + isGrounded + " " + Physics2D.Raycast(borderCheck.position, Vector2.right, 2f, groundMask).point + " " + Physics2D.Raycast(borderCheck.position, Vector2.left, 2f, groundMask).point);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
