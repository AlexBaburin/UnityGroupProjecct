using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementOfDetailsShop : MonoBehaviour
{
    public float speed = 3f;
    public float range = 1f;

    int dir = 1;
    float StartY;
    void Start()
    {
        StartY = transform.position.y;
    }

    void FixedUpdate()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime * dir);
        if (transform.position.y < StartY || transform.position.y > StartY + range)
            dir *= -1;
    }
}
