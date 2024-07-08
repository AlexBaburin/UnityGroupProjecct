using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    public Animator animator;
    public int direction = 1;
    public AudioSource source;
    public AudioClip clip;
    
    private void Jump()
    {
        clip = (AudioClip)Resources.Load("Sounds/jump");
        source.clip = clip;
        source.Play();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Rigidbody2D playerComponent = GameObject.FindGameObjectsWithTag("Player")[0].GetComponents<Rigidbody2D>()[0];
            PlayerMovement jumpForce = GameObject.FindGameObjectsWithTag("Player")[0].GetComponents<PlayerMovement>()[0];
            playerComponent.velocity = new Vector2(playerComponent.velocity.x, jumpForce.jumpForce * 1.3f);
            Jump();
            animator.SetBool("isCollision", true);
            Invoke("AnimationEnded", 0.3f);
        }
    }
    public void AnimationEnded()
    {
        animator.SetBool("isCollision", false);

    }
    bool AnimatorIsPlaying(string stateName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }
}
