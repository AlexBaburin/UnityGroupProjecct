using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    Component PlayerDamage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Debug.Log("Entered in shop");
            switch(gameObject.tag)
            {
                case "strength":
                    if (Coin.coins >= 2)
                    {
                        Debug.Log("bought a thing");
                        Coin.coins -= 2;
                        AttackArea area = GameObject.FindGameObjectsWithTag("Player")[0].GetComponentsInChildren<AttackArea>(true)[0];
                        area.damage *= 2;
                        Destroy(gameObject);
                    }
                    break;
                case "hp":
                    Debug.Log("Entered in hp");
                    if (Coin.coins >= 4)
                    {
                        Debug.Log("bought a thing");
                        Coin.coins -= 4;
                        HealthWithBlock area = GameObject.FindGameObjectsWithTag("Player")[0].GetComponents<HealthWithBlock>()[0];

                        Vector3 scale = area.healthBar.transform.localScale;
                        scale.x *= 1.3f;
                        area.healthBar.transform.localScale = scale;

                        Vector3 newPosition = area.healthBar.transform.position + new Vector3(1.48f, 0f, 0f);
                        area.healthBar.transform.position = newPosition;

                        area.healthBar.maxValue *= 1.3f;
                        area.healthBar.value = area.healthBar.maxValue;
                        area.health = area.healthBar.maxValue;
                        Destroy(gameObject);
                    }
                    break;
            }
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
