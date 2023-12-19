using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
            Invoke("DestroyObject", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyObject () {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Player") {
            Destroy(gameObject);
        }
    }
}
