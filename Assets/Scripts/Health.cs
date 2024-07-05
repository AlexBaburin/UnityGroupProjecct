using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    public float health = 100;
    public float iFrames = 100;
    public float damage = 0f;
    bool isIFramesActive = false;
    float frameTimer = 0f;

    public Slider healthBar;

    // Start is called before the first frame update
    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (damage > 0 && health > 0 && !isIFramesActive)
        {
            health -= damage;
            healthBar.value = health;
            isIFramesActive = true;
        }
        if (isIFramesActive)
        {
            if (frameTimer > iFrames)
            {
                isIFramesActive = false;
                frameTimer = 0f;
            }
            else
                frameTimer++;
        }
        if (health < 0)
            health = 0;

        damage = 0f;
    }
}
