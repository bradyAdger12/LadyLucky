using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    // Start is called before the first frame update
    public GameLogic gameLogic;
    private AudioSource levelCompleteAudioSource;
    void Start()
    {
        levelCompleteAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            gameLogic.playerIsDead = true;
            levelCompleteAudioSource.Play();
            Invoke("LoadScene", 3f);
        }
    }
}
