using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera cam;
    private Vector3 lastCameraPosition;
    [SerializeField]
    private Vector2 parallax;
    private SpriteRenderer sprite;
    void Start()
    {
        cam = Camera.main;
        lastCameraPosition = cam.transform.position;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Vector3 deltaMovement = cam.transform.position - lastCameraPosition;
        // transform.position += new Vector3(deltaMovement.x * parallax.x, deltaMovement.y * parallax.y, 0);
        // lastCameraPosition = cam.transform.position;
    }
}
