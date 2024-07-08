using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    static public int coins = 0;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            coins++;
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<HealthWithBlock>().health <= 0 ||
            SceneManager.GetActiveScene().buildIndex != 1)
            coins = 0;
    }
}