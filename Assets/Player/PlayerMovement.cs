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
    public LayerMask mask;

    private void Awake()
    {
        input = GetComponent<InputManager>();
    }

    private void Update () {
         if (Physics2D.Raycast(transform.position, transform.up * -1, transform.localScale.y / 1.5f, mask))
        {
            isGrounded = true;
        } else {
            isGrounded = false;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(input.moveVector.x * speed, 0, 0) * Time.fixedDeltaTime;
    }


    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
