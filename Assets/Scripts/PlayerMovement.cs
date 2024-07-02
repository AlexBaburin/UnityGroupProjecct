using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerControls controls;
    float direction = 0;

    public float speed = 400;
    public float jumpForce = 6;
    bool isFacingRight = true;
    bool isGrounded = false;
    bool isJumping = false;
    bool isJumpCancelled = false;
    bool isAttacking = false;
    bool isAttackQueued = false;
    int attackCounter = 0;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public Rigidbody2D playerRB;
    public Animator animator;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Enable();
        controls.Grounded.Move.performed += ctx =>
        {
            direction = ctx.ReadValue<float>();
            //Debug.Log(direction);
        };

        animator.SetBool("noBlood", true);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        //Debug.Log(isGrounded);

        isAttacking = (AnimatorIsPlaying("Attack1") || AnimatorIsPlaying("Attack2") || AnimatorIsPlaying("Attack3"));
        //Debug.Log(isAttacking);
        animator.SetBool("Attacking", isAttacking);

        //Attack
        if (attackCounter >= 3)
            attackCounter = 0;
        if (controls.Grounded.Attack.WasReleasedThisFrame())
        {
            if (!isAttacking)
            {
                attackCounter++;
                animator.SetTrigger("Attack" + attackCounter);
            }
            else
                isAttackQueued = true;
        }
        if (!isAttacking && isAttackQueued)
        {
            attackCounter++;
            animator.SetTrigger("Attack" + attackCounter);
            isAttackQueued = false;
        }

        //Jump
        if (controls.Grounded.Jump.IsPressed() && isGrounded && !isJumping)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
            isJumping = true;
        }
        if (controls.Grounded.Jump.WasReleasedThisFrame())
        {
            isJumpCancelled = true;
            //Debug.Log("Jump cancelled");
        }
        if (isJumpCancelled && playerRB.velocity.y < jumpForce * 0.75)
        {
            isJumpCancelled = false;
            isJumping = false;
            if (playerRB.velocity.y > 0)
                playerRB.velocity = new Vector2(playerRB.velocity.x, 0);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Speed control
        if (!controls.Grounded.Move.inProgress)
        {
            direction = 0;
        }

        if (direction > 0)
        {
            direction = 1;
        }
        else if (direction < 0)
        {
            direction = -1;
        }
        if (isAttacking)
        {
            if (isGrounded)
                playerRB.velocity = new Vector2(playerRB.velocity.x * 0.9f, playerRB.velocity.y);
            if (Mathf.Abs(playerRB.velocity.x) < 0.5)
                playerRB.velocity = new Vector2(0, playerRB.velocity.y);
        }
        else
            playerRB.velocity = new Vector2(direction * speed * Time.fixedDeltaTime, playerRB.velocity.y);

        animator.SetFloat("Speed", Mathf.Abs(direction));
        animator.SetBool("Grounded", isGrounded);
        animator.SetFloat("AirSpeedY", playerRB.velocity.y);

        if (isFacingRight && direction < 0 || !isFacingRight && direction > 0)
        {
            Flip();
        }
    }

    void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        isFacingRight = !isFacingRight;
    }

    bool AnimatorIsPlaying(string stateName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }
}
