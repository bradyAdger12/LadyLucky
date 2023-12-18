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
    private float radius = 5f;
    void Start()
    {
        ShootProjectile();
    }

    // Update is called once per frame
    void Update()
    {

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
            else
            {
                Debug.LogWarning("Rigidbody component not found on the projectile prefab.");
            }
        }
    }
}
