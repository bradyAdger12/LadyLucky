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
        if (SceneManager.GetSceneByBuildIndex(nextSceneBuildIndex).IsValid())
        {
            SceneManager.LoadScene(nextSceneBuildIndex);
        }
    }

    public void incrementScore()
    {
        score += 1;
        scoreText.SetText(score.ToString());
    }

    public void LoseLife(bool fellOffMap = true)
    {
        numLives -= 1;
        lives[numLives].SetActive(false);
        if (fellOffMap || numLives == 0)
        {
            Respawn(numLives == 0);
        }
        if (numLives == 0)
        {
            scoreText.SetText("0");
        }
    }

    public void Respawn(bool playerIsDead = false)
    {
        if (Checkpoint.availableCheckpoints.Count > 0)
        {
            if (playerIsDead)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            Checkpoint checkpoint = playerIsDead ? Checkpoint.availableCheckpoints.First() : Checkpoint.availableCheckpoints.Last();
            player.transform.position = new Vector3(checkpoint.transform.position.x, checkpoint.transform.position.y + 3, player.transform.position.z);

        }
    }
}
