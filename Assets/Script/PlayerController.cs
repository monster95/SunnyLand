using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;
    public enum LookStatus { Left,Right};
    public enum MoveStatus { Idle,Run,Jump,Fall,Crouch,Hurt};
    public Text healthText;
    public GameManager gameManager;
    public AudioClip jumpClip;
    public AudioClip hurtClip;
    public LayerMask layer;
    float hurtTime = 0.5f;
    float currentTime = 0f;
    float nextTime = 0f;
    int life = 2;

    Animator anim;
    Collider2D coll;

    Rigidbody2D rb;
    LookStatus lookstatus = LookStatus.Right;
    MoveStatus moveStatus = MoveStatus.Idle;
    void Start()
    {
        life = 2;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<CircleCollider2D>();
        healthText.text = "X " + life.ToString();
    }

    private void Update()
    {
        if (life <= 0)
        {
            gameManager.ChangeScene(1);
        }
    }

    void FixedUpdate()
    {
        if(moveStatus == MoveStatus.Hurt)
        {
            currentTime += Time.fixedDeltaTime;
            if(currentTime>nextTime)
            {
                moveStatus = MoveStatus.Idle;
            }
            return;
        }
        Movement();
        Jump();
        Crouch();
        SwitchAnim();
    }

    //移动
    void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal < 0 && lookstatus == LookStatus.Right)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            lookstatus = LookStatus.Left;

        }            
        else if (horizontal > 0 && lookstatus == LookStatus.Left)
        {
            transform.localScale = new Vector3(1, 1, 1);
            lookstatus = LookStatus.Right;
        }
        rb.velocity = new Vector2(horizontal * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(horizontal));
    }

    //跳跃
    void Jump()
    {
        if (Input.GetButton("Jump")&&coll.IsTouchingLayers(layer))
        {
            gameManager.audioManager.PlayOneShot(jumpClip, 2f);
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed * Time.fixedDeltaTime);
        }           
    }

    void SwitchAnim()
    {
        if (rb.velocity.y > 0)
        {
            moveStatus = MoveStatus.Jump;
            anim.SetTrigger("Jump");
        }
        else if (rb.velocity.y < 0)
        {
            moveStatus = MoveStatus.Fall;
            anim.SetTrigger("Fall");
        }
        else
        {
            moveStatus = MoveStatus.Idle;
            anim.SetTrigger("Land");
        }
    }

    void Crouch()
    {
        if (rb.velocity.y == 0 && Input.GetButton("Crouch"))
        {
            anim.SetBool("Crouch", true);
            if (moveStatus != MoveStatus.Crouch)
            {
                moveSpeed /= 2;
                moveStatus = MoveStatus.Crouch;
            }
        }            
        else
        {
            anim.SetBool("Crouch", false);
            if(moveStatus == MoveStatus.Crouch)
            {
                moveSpeed *= 2;
                moveStatus = MoveStatus.Idle;
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            if(rb.velocity.y<0)
            {
                gameManager.audioManager.PlayOneShot(jumpClip, 2f);
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed * Time.fixedDeltaTime*0.5f);
                collision.gameObject.GetComponent<Enemy>().Death();
            }
            else
            {
                life--;
                healthText.text = "X " + life.ToString();
                gameManager.audioManager.PlayOneShot(hurtClip, 2f);
                if(collision.transform.position.x>=transform.position.x)
                {
                    if(currentTime>=nextTime)
                    {
                        nextTime += hurtTime;
                        rb.velocity = new Vector2(-1f*moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
                        moveStatus = MoveStatus.Hurt;
                        anim.SetTrigger("Hurt");
                    }
                }
                else
                {
                    if (currentTime >= nextTime)
                    {
                        nextTime += hurtTime;
                        rb.velocity = new Vector2(1f * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
                        moveStatus = MoveStatus.Hurt;
                        anim.SetTrigger("Hurt");
                    }
                }
            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
