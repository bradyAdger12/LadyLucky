using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
    // Start is called before the first frame update
    private InputManager input;
    [SerializeField]
    private float speed = 3f;
    private Rigidbody2D rb;
    [HideInInspector]
    public bool isGrounded = false;
    public float jumpForce = 10f;
    private int jumps;
    public LayerMask groundMask;
    public LayerMask enemyMask;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    [SerializeField]
    private GameLogic gameLogic;
    private PlayerAudio playerAudio;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumps = 0;
        input = GetComponent<InputManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAudio = GetComponent<PlayerAudio>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetInteger("IsRunning", Math.Abs(Mathf.RoundToInt(input.moveVector.x)));
        if (Physics2D.Raycast(transform.position, transform.up * -1, transform.localScale.y / 1.5f, groundMask))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        if (!gameLogic.playerIsDead) {
            HandleSpriteRenderer();
            transform.position += new Vector3(input.moveVector.x * speed, 0, 0) * Time.fixedDeltaTime;
        }
    }

    private void HandleSpriteRenderer()
    {
        if (input.moveVector.x < 0f && !spriteRenderer.flipX)
        {
            spriteRenderer.flipX = true;
        }
        else if (input.moveVector.x > 0f && spriteRenderer.flipX)
        {
            spriteRenderer.flipX = false;
        }
    }


    public void Jump()
    {
        if (isGrounded)
        {
            jumps = 0;
        }
        if (isGrounded || (!isGrounded && jumps < 2))
        {
            float doubleJumpVelocity = 1.1f;
            rb.AddForce(Vector2.up * jumpForce * doubleJumpVelocity, ForceMode2D.Impulse);
            animator.SetTrigger("Jump");
            jumps += 1;
            playerAudio.PlayJumpSound();
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {

        // Player lands on top of enemy
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") && Physics2D.Raycast(transform.position, Vector2.down, transform.localScale.y, enemyMask)) {
            rb.AddForce(Vector2.up * 8, ForceMode2D.Impulse);
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            playerAudio.PlayKillingEnemySound();
            enemy.Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Player falls off map
        if (other.gameObject.name == "FallBox")
        {
            gameLogic.LoseLife(true);
            if (gameLogic.numLives == 0) {
                animator.SetTrigger("Die");
                playerAudio.PlayPlayerDeadAudio();
            }
        }

        // Player is struck by projectile
        else if (other.gameObject.layer == LayerMask.NameToLayer("Hit"))
        {
            gameLogic.LoseLife();
            if (gameLogic.numLives == 0)
            {
                animator.SetTrigger("Die");
                playerAudio.PlayPlayerDeadAudio();
            }
            else
            {
                animator.SetTrigger("Hurt");
                playerAudio.PlayPlayerHitAudio();
            }
        }
    }
}
