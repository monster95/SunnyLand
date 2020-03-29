using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : Enemy
{
    public float speed = 2f;
    Rigidbody2D rb;
    float up, down;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        up = transform.position.y + 3f;
        down = transform.position.y - 3f;
        rb.velocity = new Vector2(0f,speed * 1f);
    }

    void FixedUpdate()
    {
        if (rb.transform.position.y > up)
        {
            rb.velocity = new Vector2(0f,speed * -1f);
        }
        else if (rb.transform.position.y < down)
        {
            rb.velocity = new Vector2(0f,speed * 1f);
        }
    }
}
