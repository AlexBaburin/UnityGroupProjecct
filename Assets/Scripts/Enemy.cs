using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    Transform target;
    public Transform borderCheck;
    public Rigidbody2D rb;
    public LayerMask layerMask, groundMask;
    public Animator animator;
    public int enemyHP = 70;
    public Slider enemyHealthBar;
    // Start is called before the first frame update
    void Start()
    {
        enemyHealthBar.value = enemyHP;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        Physics2D.IgnoreCollision(target.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-5, 5);
        }
        else
        {
            transform.localScale = new Vector3(5, 5);
        }
    }
    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    public void TakeDamage(int damageAmount)
    {
        enemyHP -= damageAmount;
        enemyHealthBar.value = enemyHP;
        if (enemyHP > 0)
        {
            //trigger animation of hits
        }
        else
        {
            //trigger animation of death
        }
    }
}
