using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamaged : MonoBehaviour
{
    public HealthWithBlock hp;
    public Animator animator;
    public bool isDead = false;

    float healthValue;
    void Start()
    {
        hp = GetComponent<HealthWithBlock>();
        healthValue = hp.health;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp.health == 0 && !isDead)
        {
            animator.SetTrigger("Death");
            isDead = true;
        }
        else if (healthValue > hp.health)
        {
            animator.SetTrigger("Hurt");
        }
        healthValue = hp.health;
    }
}
