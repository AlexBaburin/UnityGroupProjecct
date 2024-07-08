using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthWithBlock : MonoBehaviour
{
    public bool isBlocking = false;
    public Animator animator;
    PlayerControls controls;
    public float health = 100;
    public float iFrames = 100;
    public float damage = 0f;
    bool isIFramesActive = false;
    float frameTimer = 0f;

    int staminaDamageAmount = 22;

    AttackScript attackScript;
    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        controls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().controls;

        attackScript = GetComponent<AttackScript>();
    }

    private void Awake()
    {
    }

        // Update is called once per frame
    void Update()
    {
        if ((controls.Grounded.Block.IsPressed() && !(AnimatorIsPlaying("Hurt") || AnimatorIsPlaying("Wall Slide")) && health > 0 ||
            AnimatorIsPlaying("Block")) && !attackScript.isAttacking)
        {
            animator.SetBool("Blocking", true);
            if (!AnimatorIsPlaying("Block") && !AnimatorIsPlaying("Idle Block"))
                animator.SetTrigger("Block");
            isBlocking = true;
            if (damage > 0 && GetComponent<AttackScript>().stamina >= staminaDamageAmount)
            {
                GetComponent<AttackScript>().stamina -= staminaDamageAmount;
                GetComponent<AttackScript>().TimeOfCoolDownStamina = 0;
                damage = 0;
                animator.SetTrigger("Deflect");
            }
        }
        else if (health > 0)
        {
            isBlocking = false;
            animator.SetBool("Blocking", false);
        }

        //Same as Health

        if (damage > 0 && health > 0 && !isIFramesActive)
        {
            health -= damage;
            healthBar.value = health;
            isIFramesActive = true;
        }
        if (isIFramesActive)
        {
            if (frameTimer > iFrames)
            {
                isIFramesActive = false;
                frameTimer = 0f;
            }
            else
                frameTimer++;
        }
        if (health < 0)
            health = 0;

        if (health == 0)
            animator.SetBool("isDead", true);
        else
            animator.SetBool("isDead", false);
        damage = 0f;
    }

    bool AnimatorIsPlaying(string stateName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }
}
