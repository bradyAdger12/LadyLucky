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
    private bool isGrounded = false;
    public float jumpForce = 10f;
    public LayerMask groundMask;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<InputManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleSpriteRenderer();
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
        transform.position += new Vector3(input.moveVector.x * speed, 0, 0) * Time.fixedDeltaTime;
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
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetTrigger("Jump");
        }
    }
}
