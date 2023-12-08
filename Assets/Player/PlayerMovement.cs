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

    private void Awake () {
        input = GetComponent<InputManager>();
    }

    private void Start () {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
       rb.velocity = new Vector2(input.moveVector.x * speed, rb.velocity.y);
    }


    public void Jump() {
        if (isGrounded) {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }


    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) {
            isGrounded = false;
        }
    }
}
