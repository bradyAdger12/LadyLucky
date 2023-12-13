using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    public GameLogic gameLogic;
    public AudioSource coinGrabbed;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DestroyObject () {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Player" && spriteRenderer.enabled) {
            coinGrabbed.Play();
            spriteRenderer.enabled = false;
            Invoke("DestroyObject", 2f);
            gameLogic.incrementScore();
        }
    }
}
