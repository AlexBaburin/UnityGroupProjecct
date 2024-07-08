using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;

    private void Tp()
    {
        clip = (AudioClip)Resources.Load("Sounds/teleport");
        source.clip = clip;
        source.Play();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Tp();
            Rigidbody2D playerComponent = GameObject.FindGameObjectsWithTag("Player")[0].GetComponents<Rigidbody2D>()[0];
            playerComponent.position = new Vector2(playerComponent.position.x, playerComponent.position.y + 20f);
        }
    }
}
