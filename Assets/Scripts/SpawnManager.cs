using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] powerups;
    private float zEnemySpawn = 30.0f;
    private float xSpawnRange = 5.5f;
    private float ySpawnRange = 3.0f;
    //chance 7.9f to place powerup whereever player is in Z-axis
    private float zSpawn = 7.0f;
    private float zPowerupRange = 7.0f;

    private float powerupSpawnTime = 10.0f;
    private float enemySpawnTime = 2.0f;
    private float startDelay = 1.0f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;

    public InputField playerName;

    public GameObject HighscoresUI;
    public GameObject startText;
    public Button restartButton;
    public Button startButton;


    public Button sendScoreButton;
    private int score;
    public bool isGameActive;
    private float spawnRate = 3.0f;
    private Highscores highscoresScript;

    [SerializeField] float spawnTimer;
    float timer = 0f;
    int wave;

    void Start()
    {
        /*
        wave = 1;
        isGameActive = true;
        StartCoroutine(SpawnEnemy());
        score = 0;
        scoreText.text = "Score: " + score;
        UpdateScore(0);
        highscoresScript = GameObject.Find("Highscores").GetComponent<Highscores>();
        */
        //InvokeRepeating("SpawnEnemy", startDelay, enemySpawnTime);
        //destroy Powerup after x seconds, increase speed of enemy?
        //make Enemy fire?
        //InvokeRepeating("SpawnPowerup", startDelay, powerupSpawnTime);

    }

    // Update is called once per frame
    void Update()
    {
        //print("timer: " + timer);

        timer += Time.deltaTime;

        if (timer > 5)
        {
            print("5 secs passed");
            wave = 2;

        }
        else if (timer > 10)
        {
            print("10 secs passed");
            wave = 2;

        }
    }
    public void RestartGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);


    }

    public void SendPlayerScore()
    {

        highscoresScript.AddNewHighscore(playerName.text, score);
        sendScoreButton.gameObject.SetActive(false);


    }

    public void StartGame()
    {
        wave = 1;
        isGameActive = true;
        StartCoroutine(SpawnEnemy());
        score = 0;
        scoreText.text = "Score: " + score;
        UpdateScore(0);
        highscoresScript = GameObject.Find("Highscores").GetComponent<Highscores>();
        startButton.gameObject.SetActive(false);
        startText.gameObject.SetActive(false);

    }
    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        HighscoresUI.gameObject.SetActive(true);


    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }


    IEnumerator SpawnEnemy()
    {
        while (isGameActive)
        {
            {
                yield return new WaitForSeconds(spawnRate);
                //Test UpdateScore(5);
                float randomX = Random.Range(-xSpawnRange, xSpawnRange);
                float randomY = Random.Range(-ySpawnRange, ySpawnRange);

                int randomIndex = Random.Range(0, enemies.Length);
                Vector3 spawnPos = new Vector3(randomX, randomY, zEnemySpawn);
                if (wave == 1)
                {
                    Instantiate(enemies[randomIndex], spawnPos, enemies[randomIndex].gameObject.transform.rotation);
                }
                else if (wave == 2)
                {
                    Instantiate(enemies[0], spawnPos, enemies[0].gameObject.transform.rotation);
                    Instantiate(enemies[1], spawnPos, enemies[1].gameObject.transform.rotation);
                }
                else if (wave == 3)
                {
                    Instantiate(enemies[0], spawnPos, enemies[0].gameObject.transform.rotation);
                    Instantiate(enemies[1], spawnPos, enemies[1].gameObject.transform.rotation);
                    Instantiate(enemies[2], spawnPos, enemies[1].gameObject.transform.rotation);
                }
            }
        }

        /* void SpawnPowerup()
         {
             while (isGameActive)
             {
                 float randomX = Random.Range(-xSpawnRange, xSpawnRange);
                 float randomY = Random.Range(-ySpawnRange, ySpawnRange);

                 Vector3 spawnPos = new Vector3(randomX, randomY, zSpawn);

                 Instantiate(powerups[0], spawnPos, powerups[0].gameObject.transform.rotation);
             }
         }*/

    }
}


