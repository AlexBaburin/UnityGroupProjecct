using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    int staminaDamageAmount = 15;
    public float TimeOfCoolDownStamina = 0f;
    float TimeOfResumingStamina = 0f;
    public int stamina = 100;
    public int staminaResuming = 1;
    public float staminaCoolDown = 2.5f;

    public Animator animator;
    public Slider PlayerStaminaBar;
    HealthWithBlock hp;

    // Start is called before the first frame update
    void Start()
    {
        controls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().controls;
        PlayerStaminaBar.value = stamina;
        hp = GetComponent<HealthWithBlock>();
    }

    private void Awake()
    {
        
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
        if (controls.Grounded.Attack.WasPressedThisFrame() && !hp.isBlocking && hp.health > 0)
        {
            if (!isAttacking && !isAttackQueued && stamina - staminaDamageAmount > 0)
            {
                stamina -= staminaDamageAmount;
                TimeOfCoolDownStamina = 0;
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
        else if (!isAttacking && isAttackQueued && stamina - staminaDamageAmount > 0)
        {
            stamina -= staminaDamageAmount;
            TimeOfCoolDownStamina = 0;
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
        else
            if (stamina - staminaDamageAmount <= 0)
                isAttackQueued = false;
        if (stamina >= 0)
            PlayerStaminaBar.value = stamina;
        else
        {
            stamina = 0;
            PlayerStaminaBar.value = 0;
        }
        if (stamina < 100 && TimeOfCoolDownStamina > staminaCoolDown)
        {
            TimeOfResumingStamina += Time.deltaTime;
            if (TimeOfResumingStamina > 0.01)
            {
                stamina += staminaResuming;
                TimeOfResumingStamina = 0;
            }
        }
        else
        {
            TimeOfResumingStamina = 0;
        }
        TimeOfCoolDownStamina += Time.deltaTime;
    }

    bool AnimatorIsPlaying(string stateName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }
}
