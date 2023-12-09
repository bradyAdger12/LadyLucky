using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement player;
    [SerializeField]
    public GameObject virtualCamera;
    private bool playerDead;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void NextLevel () {
        
    }

    public void SetPlayerDead(bool value)
    {
        playerDead = value;
        if (Checkpoint.availableCheckpoints.Count > 0) {
            Checkpoint lastCheckpoint = Checkpoint.availableCheckpoints.Last();
            player.transform.position = new Vector3(lastCheckpoint.transform.position.x, lastCheckpoint.transform.position.y + 1, player.transform.position.z);
        }
    }
}
