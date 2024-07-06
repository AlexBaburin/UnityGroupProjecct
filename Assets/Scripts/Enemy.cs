using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Threading;
public class Enemy : MonoBehaviour
{
    Transform target;
    public Transform borderCheck, groundCheck;
    public Rigidbody2D rb;
    public LayerMask layerMask, groundMask;
    public Animator animator;
    public Slider enemyHealthBar;
    public Health healthEnemy;
    public GameObject attackArea;
    float deathTimer = 0f;
    float timeUnitilDespawn = 3f;
    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("isDead", false);
        enemyHealthBar.value = healthEnemy.health;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        Physics2D.IgnoreCollision(target.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!animator.GetBool("isDead"))
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

        if (animator.GetBool("isDead") == true)
        {
            deathTimer += Time.fixedDeltaTime;
        }
        if (deathTimer > timeUnitilDespawn)
        {
            Destroy(gameObject);
        }
    }
    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
