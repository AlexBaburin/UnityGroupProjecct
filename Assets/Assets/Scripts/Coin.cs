using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    static public int coins = 0;
    private void Awake()
    {
        coins = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            coins++;
            Destroy(gameObject);
        }
    }
}