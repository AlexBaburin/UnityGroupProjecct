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

    public Slider healthBar;

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
    void FixedUpdate()
    {
        if (controls.Grounded.Block.IsPressed() && !(AnimatorIsPlaying("Hurt") || AnimatorIsPlaying("Wall Slide")) && health > 0 ||
            AnimatorIsPlaying("Block"))
        {
            animator.SetBool("IdleBlock", true);
            isBlocking = true;
            if (damage > 0)
            {
                damage = 0;
                animator.SetTrigger("Block");
            }
        }
        else if (health > 0)
        {
            isBlocking = false;
            animator.SetBool("IdleBlock", false);
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

        damage = 0f;
    }

    bool AnimatorIsPlaying(string stateName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }
}
