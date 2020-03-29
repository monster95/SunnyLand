using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : Enemy
{
    public float speed = 2f;
    Rigidbody2D rb;
    float left, right;
    bool lookLeft = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        left = transform.position.x-4f;
        right = transform.position.x+4f;
        rb.velocity = new Vector2(speed*-1, 0f);
    }

    void FixedUpdate()
    {
        if(lookLeft&&rb.transform.position.x<left)
        {
            rb.velocity = new Vector2(speed * 1f, 0f);
            transform.localScale = new Vector3(-1, 1, 1);
            lookLeft = false;
        }
        else if(!lookLeft&&rb.transform.position.x>right)
        {
            rb.velocity = new Vector2(speed * -1f, 0f);
            transform.localScale = new Vector3(1, 1, 1);
            lookLeft = true;
        }
    }

}
