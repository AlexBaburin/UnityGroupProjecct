using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioSource source;
    Animator animator;
    AudioClip clip;
    bool enter = false;
    bool jump = false;
    float timer = 0f;
    float cooldownTimer = 0f;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    public void StepSound()
    {
        clip = (AudioClip)Resources.Load("Sounds/step");
        source.clip = clip;
        source.Play();
    }

    public void Attack()
    {
        clip = (AudioClip)Resources.Load("Sounds/player_attack");
        source.clip = clip;
        source.Play();
    }

    public void Deflect()
    {
        clip = (AudioClip)Resources.Load("Sounds/deflect");
        source.clip = clip;
        source.Play();
    }

    public void Block()
    {
        clip = (AudioClip)Resources.Load("Sounds/block");
        source.clip = clip;
        source.Play();
    }

    public void Hit()
    {
        clip = (AudioClip)Resources.Load("Sounds/hit");
        source.clip = clip;
        source.Play();
    }


    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            jump = false;
            if (!enter)
                timer = cooldownTimer;
            enter = true;
            timer += Time.deltaTime;
            if (timer > 0.33f)
            {
                source.volume = 0.5f;
                source.pitch = Random.Range(0.7f, 1.3f);
                StepSound();
                timer = 0f;
                cooldownTimer = 0f;
            }
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            if (!jump)
            {
                jump = true;
                source.volume = 0.5f;
                source.pitch = Random.Range(0.7f, 1.3f);
                StepSound();
            }
        }
        else
        {
            jump = false;
            if (!source.isPlaying)
            {
                source.pitch = 1f;
                source.volume = 0.5f;
            }
            enter = false;
            timer = 0;
            cooldownTimer += Time.deltaTime;
        }

    }

}
