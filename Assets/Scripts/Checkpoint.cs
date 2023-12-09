using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    [HideInInspector]
    public static List<Checkpoint> availableCheckpoints = new();
    private bool triggered = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player" && !triggered)
        {
            triggered = true;
            availableCheckpoints.Add(this);
        }
    }
}
