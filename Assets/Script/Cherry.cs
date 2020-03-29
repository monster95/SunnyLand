using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{
    Animator anim;
    public Collection collection;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !anim.GetBool("Feedback"))
        {
            GetComponent<BoxCollider2D>().enabled = false;

            anim.SetBool("Feedback", true);
        }
            
    }

    void Death()
    {
        collection.updateText();
        Destroy(this.gameObject);
    }
}
