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
    private int score = 0;
    private TMP_Text scoreText;
    List<GameObject> lives = new();

    public int numLives = 3;
    // Start is called before the first frame update
    void Awake()
    {
        InitLives();
        scoreText = scoreUI.GetComponent<TMP_Text>();
        scoreText.SetText("0");
        
        
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

    public void incrementScore() {
        score += 1;
        scoreText.SetText(score.ToString());
    }

    public void InitLives()
    {
        for (int i = 0; i < numLives; i++)
        {
            RectTransform rectTransform = canvas.GetComponent<RectTransform>();
            GameObject life = Instantiate(lifePrefab, new Vector3(rectTransform.position.x - (rectTransform.rect.width / 2) + ((i * 40) + 30), rectTransform.rect.height - 30, 0), Quaternion.identity);
            life.transform.SetParent(canvas.transform);
            lives.Add(life);
        }
    }

    public void LoseLife()
    {
        if (lives.Count > 0)
        {
            Destroy(lives.Last());
            lives.RemoveAt(lives.Count - 1);
            Respawn(lives.Count == 0);
            if (lives.Count == 0)
            {
                scoreText.SetText("0");
                InitLives();
            }
        }
    }

    public void Respawn(bool playerIsDead = false)
    {
        if (Checkpoint.availableCheckpoints.Count > 0)
        {
            Checkpoint checkpoint = playerIsDead ? Checkpoint.availableCheckpoints.First() : Checkpoint.availableCheckpoints.Last();
            player.transform.position = new Vector3(checkpoint.transform.position.x, checkpoint.transform.position.y + 3, player.transform.position.z);

        }
    }
}
