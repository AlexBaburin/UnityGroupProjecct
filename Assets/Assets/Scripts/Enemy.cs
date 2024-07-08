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
    public GameObject Coin;
    public GameObject bossLadder, Ladder;
    public float speedFallLadder = 1;
    
    GameObject Boss;
    float deathTimer = 0f;
    float timeUnitilDespawn = 1f;
    bool lever = true;
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
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y);
            }
            else
            {
                transform.localScale = new (Mathf.Abs(transform.localScale.x), transform.localScale.y);
            }
        }

        if (animator.GetBool("isDead") == true)
        {
            deathTimer += Time.fixedDeltaTime;
        }
        if (deathTimer > timeUnitilDespawn)
        {
            if (gameObject.name == "barbarian")
            {
                if (lever)
                {
                    lever = false;
                    Boss = GameObject.FindGameObjectsWithTag("bossBar")[0];
                    Boss.SetActive(false);
                }
                bossLadder.transform.Translate(Vector2.down * speedFallLadder * Time.deltaTime);
                if (bossLadder.transform.position.y <= 0)
                {
                    Ladder.SetActive(true);
                    Destroy(gameObject);
                    Instantiate(Coin, new Vector2(transform.position.x, transform.position.y + 0.5f), Quaternion.identity);
                    Instantiate(Coin, new Vector2(transform.position.x + 1f, transform.position.y + 0.5f), Quaternion.identity);
                    Instantiate(Coin, new Vector2(transform.position.x - 1f, transform.position.y + 0.5f), Quaternion.identity);
                }
                GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<AudioSource>().mute = false;
                GameObject.FindGameObjectsWithTag("detector")[0].GetComponent<AudioSource>().mute = true;
            }
            else
            {
                Destroy(gameObject);
                Instantiate(Coin, transform.position, Quaternion.identity);
            }
        }
    }
    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
