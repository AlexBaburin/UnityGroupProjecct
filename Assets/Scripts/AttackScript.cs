using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public bool isAttacking = false;
    bool isAttackQueued = false;
    bool isAttackHitboxActive = false;
    float timer = 0f;
    float timeToAttack = 0.2f;
    float delayTimer = 0f;
    public float delay = 0.01f;
    int attackCounter = 0;

    public GameObject attackArea;
    PlayerControls controls;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(delayTimer);
        isAttacking = (AnimatorIsPlaying("Attack1") || AnimatorIsPlaying("Attack2") || AnimatorIsPlaying("Attack3"));
        //Debug.Log(isAttacking);
        animator.SetBool("Attacking", isAttacking);

        //Attack
        if (isAttacking)
        {
            timer += Time.deltaTime;
            if (timer >= timeToAttack && !isAttackHitboxActive)
            {
                isAttackHitboxActive = true;
                attackArea.SetActive(true);
            }
        }
        else
        {
            if (delay > delayTimer)
                delayTimer += Time.deltaTime;
            isAttackHitboxActive = false;
            attackArea.SetActive(false);
            timer = 0;
        }
        if (timer < timeToAttack)
        {
            isAttackHitboxActive = false;
            attackArea.SetActive(false);
        }

        if (!isAttacking && !isAttackQueued && delayTimer >= delay)
            attackCounter = 0;
        if (attackCounter > 3)
            attackCounter = 0;
        if (controls.Grounded.Attack.WasPressedThisFrame())
        {
            if (!isAttacking && !isAttackQueued)
            {
                attackCounter++;
                if (attackCounter > 3)
                    attackCounter = 1;
                animator.SetTrigger("Attack" + attackCounter);
                animator.SetBool("Attacking", true);
                timer = 0;
                delayTimer = 0;
            }
            else
                isAttackQueued = true;
        }
        else if (!isAttacking && isAttackQueued)
        {
            attackCounter++;
            if (attackCounter > 3)
                attackCounter = 1;
            animator.SetTrigger("Attack" + attackCounter);
            animator.SetBool("Attacking", true);
            attackArea.SetActive(true);
            isAttackQueued = false;
            timer = 0;
            delayTimer = 0;
        }
    }

    bool AnimatorIsPlaying(string stateName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }
}
