using System.Collections;
using UnityEngine;
public class Player : MonoBehaviour
{

    float health = 100;

    float moveSpeed = 5f;
    float jumpForce = 10f;

    public Transform groundCheck;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    public bool isGrounded;
    
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sr;

    int extraJumpValue = 1;
    private int extraJump;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        extraJump = extraJumpValue;     
    }


    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2 (moveInput * moveSpeed, rb.linearVelocityY);

        if (isGrounded)
        {
            extraJump = extraJumpValue;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
            }
            else if(extraJump > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce);
                extraJump--;
            }
        }

        SetAnimation(moveInput);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void SetAnimation(float moveInput)
    {
        if (isGrounded)
        {
            if (moveInput == 0)
            {
                animator.Play("idle");
            }
            else
            {
                animator.Play("run");
            }
        }
        else
        {
            if(rb.linearVelocityY > 0)
            {
                animator.Play("jump_up");
            }
            else
            {
                animator.Play("jump_down");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Damage")
        {
            health -= 25;
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpForce/5);
            StartCoroutine(BlinkRed());
        }

        if(health <= 0)
        {
            Die();
        }
    }

    private IEnumerator BlinkRed()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
    }

    private void Die()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Platformer");
    }
}
