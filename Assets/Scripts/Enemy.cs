using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject projectilePrefab;
    public int numProjectiles = 8;
    public float projectileSpeed = 3f;
    private float totalAngle = 360f;
    private BoxCollider2D collider2d;
    private float radius = 5f;
    [SerializeField]
    private float dangerZoneRadius = 5f;
    [SerializeField]
    private float timeBetweenShots = 3.5f;
    public LayerMask playerMask;
    private bool shotProjectile = false;
    private bool canShootProjectile = true;
    private bool isDead = false;
    
    void Start()
    {
        collider2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canShootProjectile) {
            RaycastHit2D raycastHit2D = Physics2D.CircleCast(transform.position, dangerZoneRadius, Vector2.zero, 0f, playerMask);
            if (raycastHit2D.collider != null && !shotProjectile)
            {
                InvokeRepeating("ShootProjectile", 0f, timeBetweenShots);
                shotProjectile = true;
            } else if (raycastHit2D.collider == null) {
                shotProjectile = false;
                CancelInvoke("ShootProjectile");
            }
        }

        if (isDead) {
            canShootProjectile = false;
            collider2d.isTrigger = true;
            transform.localScale -= new Vector3(.07f, .07f, 0);
            if (transform.localScale.x < 0) {
                Destroy(gameObject);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, dangerZoneRadius);
    }

    public void Die () {
        isDead = true;
    }

    void ShootProjectile()
    {
        Vector3 center = transform.position;

        float angleStep = totalAngle / numProjectiles;

        for (int i = 0; i < numProjectiles; i++)
        {
            float angle = i * angleStep;
            float xPos = center.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float yPos = center.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector3 projectilePosition = new Vector3(xPos, yPos, 0f);

            GameObject newProjectile = Instantiate(projectilePrefab, center, Quaternion.identity);
            Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Vector3 shootDirection = (projectilePosition - center).normalized;
                rb.velocity = shootDirection * projectileSpeed;
            }
        }
    }
}
