using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UIElements;
public class PlayerMovement : MonoBehaviour
{
    public PlayerControls controls;
    float direction = 0;

    public float speed = 400;
    public float MaxSpeed = 1200;
    public float jumpForce = 6;


    bool isFacingRight = true;
    bool isGrounded = false;
    bool isJumping = false;
    bool isNearWall = false;
    bool isJumpCancelled = false;
    bool isDead = false;

    AttackScript attack;
    PlayerDamaged playerDamaged;
    HealthWithBlock hp;

    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask groundLayer;

    public Rigidbody2D playerRB;
    public Animator animator;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Grounded.Enable();
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
        attack = GetComponent<AttackScript>();
        playerDamaged = GetComponent<PlayerDamaged>();
        hp = GetComponent<HealthWithBlock>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        isNearWall = Physics2D.OverlapCircle(wallCheck.position, 0.1f, groundLayer);
        isDead = playerDamaged.isDead;

        animator.SetBool("WallSlide", isNearWall);

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
        if (attack.isAttacking || isDead || hp.isBlocking)
        {
                if (isGrounded)
                    playerRB.velocity = new Vector2(playerRB.velocity.x * 0.9f, playerRB.velocity.y);
                if (Mathf.Abs(playerRB.velocity.x) < 0.5)
                    playerRB.velocity = new Vector2(0, playerRB.velocity.y);
            if (isDead)
            {
                direction = 0;
                playerRB.velocity = new Vector2(playerRB.velocity.x, 0);
            }
        }
        else
            playerRB.velocity = new Vector2(direction * speed * Time.fixedDeltaTime, playerRB.velocity.y);

        if (playerRB.velocity.y > MaxSpeed)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, MaxSpeed);
        }
        if (playerRB.velocity.x < MaxSpeed * -1)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, MaxSpeed * -1);
        }
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
}
