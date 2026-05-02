using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;
    public GameObject cloudPrefab;
    public GameObject coinPrefab;
    public GameObject heartPrefab;
    public GameObject powerupPrefab;
    public GameObject audioPlayer;

    public AudioClip powerupSound;
    public AudioClip powerdownSound;
    public AudioClip explosionSound;

    public GameObject gameOverScreen;

    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI powerupText;

    public float horizontalScreenSize;
    public float verticalScreenSize;

    public int score;
    private bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;
        score = 0;
        gameOver = false;

        Instantiate(playerPrefab, transform.position, Quaternion.identity);
        CreateSky();
        
        InvokeRepeating("CreateCoins", 5, 10);
        InvokeRepeating("CreateHeart", 15, 15);
        InvokeRepeating("CreateEnemyOne", 1, 2);
        InvokeRepeating("CreateEnemyTwo", 2, 5);

        StartCoroutine(SpawnPowerup());
        powerupText.text = "No powerups yet!";
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void CreateEnemyOne()
    {
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f, verticalScreenSize, 0), Quaternion.identity);
    }
    void CreateEnemyTwo()
    {
        Instantiate(enemyTwoPrefab, new Vector3(Random.Range(-6f, 6f), 6.5f, 0), Quaternion.identity);
    }

    void CreatePowerup()
    {
        Instantiate(powerupPrefab, new Vector3(Random.Range(-horizontalScreenSize * 0.6f, horizontalScreenSize * 0.6f), Random.Range(-verticalScreenSize * 0.1f, verticalScreenSize * 0.8f), 0), Quaternion.identity);
    }

    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), Random.Range(-verticalScreenSize, verticalScreenSize), 0), Quaternion.identity);
        }
    }

    void CreateCoins()
    {
        Instantiate(coinPrefab, new Vector3(Random.Range(-6f, 6f), Random.Range(0.5f, -3.5f), 0), Quaternion.identity);
    }

    void CreateHeart()
    {
        Instantiate(heartPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f, verticalScreenSize, 0), Quaternion.identity);
    }

    public void ManagePowerupText(int powerupType)
    {
        switch (powerupType)
        {
            case 1:
                powerupText.text = "Speed!";
                break;
            case 2:
                powerupText.text = "Double Weapon!";
                break;
            case 3:
                powerupText.text = "Triple Weapon!";
                break;
            case 4:
                powerupText.text = "Shield!";
                break;
            default:
                powerupText.text = "No powerups yet!";
                break;
        }
    }

    IEnumerator SpawnPowerup()
    {
        float spawnTime = Random.Range(5, 7); 
        yield return new WaitForSeconds(spawnTime);
        CreatePowerup();
        StartCoroutine(SpawnPowerup());
    }

    public void PlaySound(int whichSound)
    {
        switch (whichSound)
        {
            case 1:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerupSound);
                break;
            case 2:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerdownSound);
                break;
            case 3:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(explosionSound);
                break;
        }
    }

    public void AddScore(int earnedScore)
    {
        score = score + earnedScore;
        scoreText.text = "Score: " + score;
    }
    public void ChangeLivesText(int currentLives)
    {
        livesText.text = "Lives: " + currentLives;
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        gameOver = true;
        CancelInvoke();
    }
}