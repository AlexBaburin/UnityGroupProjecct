using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public int damage = 5;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Health>() != null)
        {
            Health health = collider.GetComponent<Health>();
            health.damage = damage;
        }
        if (collider.GetComponent<HealthWithBlock>() != null)
        {
            HealthWithBlock health = collider.GetComponent<HealthWithBlock>();
            health.damage = damage;
        }
    }
}
