using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormsController : MonoBehaviour
{
    //RBs
    [Header("Prefab Inputs")]
    public Rigidbody rbAmmo;
    public Rigidbody2D rb;
    public GameObject jumpCloudFX;
    public GameObject hitFX;

    //See if grounded
    [Header("Current State")]
    public Transform GroundCheck;
    public LayerMask groundLayers;
    private bool isKnockedBack = false;

    //Animation
    [Header("Animator")]
    public Animator animator;

    //Jumping
    [Header("Jumping Control")]
    public float jumpForce;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    Vector3 jumpSpawn = new Vector3(0, -0.25f, 0);

    //Movement
    [Header("Movement Control")]
    public float moveSpeed;
    public float AmmoForce;
    public float AmmoForceControl;
    public float MovementSmoothing;
    Vector2 movement;
    private bool isFacingRight;
    public bool isGrounded;
    private bool isJumping = false;
    public float KnockbackStr;

    //Controls
    [Header("Controls")]
    public KeyCode Jump;
    public KeyCode Left;
    public KeyCode Right;
    public KeyCode Gun;

    //Health control
    [Header("Health Controller")]
    public HealthController HealthController;


    //Initialize, assign RB and animator
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        AmmoForceControl = AmmoForce;
    }

    //Check for input, control animation
    void Update()
    {
        animator.SetBool("isWalking", true);

        //see if character is falling
        float JumpVelocity = rb.velocity.y;
        animator.SetFloat("JumpVel", JumpVelocity);

        if (isKnockedBack == false)
        {
            //send Jump event
            if (isJumping == true)
            {
                animator.SetBool("isJumping", true);
            }
            else
            {
                animator.SetBool("isJumping", false);
            }

            if (Input.GetKeyDown(Jump))
            {
                Move(0, true);
                animator.SetTrigger("Jump");
            }

            if (Input.GetKey(Right))
            {
                Move(10, false);
            }

            if (Input.GetKey(Left))
            {
                Move(-10, false);
            }

            if (Input.GetKeyDown(Gun))
            {
                Shüt();
            }
        }
       
    }

    void Shüt()
    {
        //spawn bullet
        Rigidbody clone;
        int Direction;
        if (isFacingRight == true)
        {
            Direction = 1;
        }
        else
        {
            Direction = -1;
        }


        Vector3 fix = new Vector3(1.5f * Direction, 0.5f, 0f);
        clone = Instantiate(rbAmmo, transform.position + fix, transform.rotation);
        clone.velocity = transform.TransformDirection(AmmoForce * Direction, 0f, 0f);

        Vector3 keyr = new Vector3(AmmoForce * Direction, 0, 0);
        rbAmmo.AddForce(keyr);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, groundLayers);

        if (isGrounded == true)
        {
            animator.SetTrigger("Land");
            animator.SetBool("isGrounded", true);
        }
        else
        {
            animator.SetBool("isGrounded", false);
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(Jump))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }

    }

    public void LandFX()
    {
        Instantiate(jumpCloudFX, transform.position + jumpSpawn, transform.rotation);
    }

    //Tell animator player has landed
    public void HasLanded()
    {
        isJumping = false;
        isKnockedBack = false;
    }

    public void RegainKB()
    {
        isKnockedBack = false;
    }

    public void Move(float move, bool jump)
    {
            Vector2 refVel = Vector2.zero;

            animator.SetBool("isWalking", false);
            Vector2 targetVelocity = new Vector2(move * moveSpeed, rb.velocity.y);
            rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref refVel, MovementSmoothing);

            if (move > 0 && !isFacingRight || move < 0 && isFacingRight)
            {
                // flip player
                Flip();
            }

        if (isGrounded && jump)
        {
            isJumping = true;
            Instantiate(jumpCloudFX, transform.position + jumpSpawn, transform.rotation);
            rb.AddForce(new Vector2(0f, jumpForce * 25));
        }

    }

    public void Knockback(Vector3 pos)
    {
        isKnockedBack = true;
        Vector2 MoveDirection = rb.transform.position - pos;
        rb.AddForce(MoveDirection.normalized * KnockbackStr);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bullet")
        {
            animator.SetTrigger("hurt");
            Knockback(collision.transform.position);
            Destroy(collision.gameObject);
            Instantiate(hitFX, collision.transform.position, transform.rotation);
            HealthController.Damage();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector2 thisScale = transform.localScale;
        thisScale.x *= -1;
        transform.localScale = thisScale;
    }
}