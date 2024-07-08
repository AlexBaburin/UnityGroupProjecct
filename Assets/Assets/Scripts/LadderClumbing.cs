using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClumbing : MonoBehaviour
{
    public GameObject colliderOfStep;
    public float climbSpeed = 50;
    Rigidbody2D playerComponent;
    PlayerMovement jump;
    bool isOnLadder = false;
    float inputVertical;

    public AudioSource source;
    public AudioClip clip;
    private void Start()
    {
        playerComponent = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Rigidbody2D>();
        jump = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerMovement>();
    }

    private void Climbing()
    {
        clip = (AudioClip)Resources.Load("Sounds/climbing");
        source.clip = clip;
        source.Play();
    }
    void Update()
    {
        if (isOnLadder)
        {
            Debug.Log("isOnLadder");
            Climbing();
            inputVertical = Input.GetAxis("Vertical");
            playerComponent.velocity = new Vector2(playerComponent.velocity.x, inputVertical * climbSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "ladder")
        {
            isOnLadder = true;
            colliderOfStep.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.tag == "ladder")
        {
            playerComponent.velocity = new Vector2(playerComponent.velocity.x, 1f);
            isOnLadder = false;
            Invoke("ActiveLadder", 0.1f);
        }
    }
    void ActiveLadder()
    {
        colliderOfStep.SetActive(true);
    }
}
