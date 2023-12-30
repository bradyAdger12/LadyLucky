using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement player;
    [SerializeField]
    public GameObject virtualCamera;
    public GameObject canvas;
    public GameObject lifePrefab;
    public GameObject scoreUI;
    private AudioSource backgroundAudio;
    private int score = 0;
    private TMP_Text scoreText;

    public int numLives = 3;
    [HideInInspector]
    public bool playerIsDead = false;
    private List<GameObject> lives = new();

    // Start is called before the first frame update
    void Awake()
    {
        backgroundAudio = GetComponent<AudioSource>();
        backgroundAudio.Play();
        scoreText = scoreUI.GetComponent<TMP_Text>();
        scoreText.SetText("0");
        foreach (Transform child in canvas.transform)
        {
            if (child.name.Contains("Heart"))
            {
                lives.Add(child.gameObject);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LoadNextScene()
    {
        int nextSceneBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneBuildIndex > -1)
        {
            Debug.Log("load");
            SceneManager.LoadScene(nextSceneBuildIndex);
        }
    }

    public void incrementScore()
    {
        score += 1;
        scoreText.SetText(score.ToString());
    }

    public void LoseLife(bool didFall = false)
    {
        numLives -= 1;
        lives[numLives].SetActive(false);
        if (numLives == 0)
        {
            playerIsDead = true;
            Invoke("ReloadScene", 2f);
        } if (didFall) {
            Respawn();
        }
    }

    private void ReloadScene () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Respawn()
    {
        if (Checkpoint.availableCheckpoints.Count > 0)
        {
            Checkpoint checkpoint = Checkpoint.availableCheckpoints.Last();
            player.transform.position = new Vector3(checkpoint.transform.position.x, checkpoint.transform.position.y + 3, player.transform.position.z);

        }
    }
}
