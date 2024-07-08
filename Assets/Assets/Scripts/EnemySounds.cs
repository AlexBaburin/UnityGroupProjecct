using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{

    public AudioSource source;
    Animator animator;
    AudioClip clip;
    bool enter = false;
    float timer = 0f;
    float cooldownTimer = 0f;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    public void StepSound()
    {
        clip = (AudioClip)Resources.Load("Sounds/enemy_step");
        source.clip = clip;
        source.Play();
    }

    public void SlimeHit()
    {
        clip = (AudioClip)Resources.Load("Sounds/slime_hit");
        source.clip = clip;
        source.Play();
    }

    public void EnemySword()
    {
        clip = (AudioClip)Resources.Load("Sounds/enemy_sword");
        source.clip = clip;
        source.Play();
    }

    public void EnemyFastSword()
    {
        clip = (AudioClip)Resources.Load("Sounds/enemy_fast_attack");
        source.clip = clip;
        source.Play();
    }
    public void EnemyDeath()
    {
        clip = (AudioClip)Resources.Load("Sounds/death");
        source.clip = clip;
        source.Play();
    }

    public void BeforeAttack()
    {
        clip = (AudioClip)Resources.Load("Sounds/block");
        source.clip = clip;
        source.Play();
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            if (!enter)
                timer = cooldownTimer;
            enter = true;
            timer += Time.deltaTime;
            if (timer > 0.5f)
            {
                source.pitch = Random.Range(0.7f, 1.3f);
                StepSound();
                timer = 0f;
                cooldownTimer = 0f;

            }
        }
        else
        {
            enter = false;
            timer = 0;
            cooldownTimer += Time.deltaTime;
            if (!source.isPlaying)
            {
                source.pitch = 1f;
                source.volume = 1f;
            }
        }
    }

}
