using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 flipperScale = new Vector3(-1, 1, 1);

    //收到伤害的受力方向
    bool isFacingRighrt;

    private Rigidbody2D rigi;
    private Animator animator;

    float moveSpeed = 10f;
    float jumpForce = 1f;
    float hurtForce = 1;

    bool isOnGround;
    float moveX;
    float moveY;
    int moveChangeAni;
    public GameObject hit_crackObj;
    private CinemaShaking cinemaShaking;
    private static PlayerController _instance;
    public static PlayerController Get
    {
        get
        {
            if (_instance == null)
            {
                // instance = this ;
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cinemaShaking = FindObjectOfType<CinemaShaking>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            TakeDamage();
        }
        Movement();
        Jump();
    }
    private void Movement()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        Direction();
        rigi.velocity = new Vector2(moveX*moveSpeed,rigi.velocity.y);
        if (moveX > 0)
        {
            isFacingRighrt = true;
            moveChangeAni = 1;
        }
        else if(moveX < 0)
        {
            isFacingRighrt = false;
            moveChangeAni = -1;
        }
        else
            moveChangeAni = 0;

        animator.SetInteger("movement", moveChangeAni);
    }
    private void Direction()
    {
        if (moveX > 0)
        {
            transform.localScale = flipperScale;
        }
        else if (moveX < 0)
        {
            transform.localScale = Vector3.one;
        }
    }
    private void Jump()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            rigi.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
            animator.SetTrigger("Jump_trigger");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Grounding(collision,false);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Grounding(collision, false);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Grounding(collision, true);
    }
    private void Grounding(Collision2D col, bool exitState)
    {
        if (exitState)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Terrain"))
            {
                isOnGround = false;
            }

        }
        else
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Terrain") && !isOnGround && col.contacts[0].normal == Vector2.up)
            {
                isOnGround = true;
                JumpCancel();
            }
            else if (col.gameObject.layer == LayerMask.NameToLayer("Terrain") && !isOnGround && col.contacts[0].normal == Vector2.down)
            {
                JumpCancel();
            }
        }
        animator.SetBool("IsOnground", isOnGround);
    }
    private void JumpCancel()
    {
        animator.ResetTrigger("Jump_trigger");
    }
    private void TakeDamage()
    {
        hit_crackObj.SetActive(true);
        animator.Play("TakeDamage");
        cinemaShaking.CinemaShake();
        StartCoroutine(Invincibility.Get.SetInvincibility());


        //FindObjectOfType<Health>().Hurt();
        if (isFacingRighrt)
        {
            rigi.velocity = new Vector2(1, 1) * hurtForce;
        }
        else
            rigi.velocity = new Vector2(-1, 1) * hurtForce;

    }
}
