using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI scoreText;
    public Transform player;
    private SpawnManager spawnManager;
    
    public int score = 0;
    private const int fastModeBonus = 3;
    private int scoreMultiplayer = 1;
    private int level = 1;
    private int nextLevelDistance = 1500;
    private float deathDelay = 1.5f;


    private void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            scoreMultiplayer = fastModeBonus;
        }
        else
        {
            scoreMultiplayer = 1;
        }
    }

    private void FixedUpdate()
    {
        score += level * scoreMultiplayer;
        scoreText.text = "Score: " + score.ToString();
        if(player.position.z > nextLevelDistance)
        {
            nextLevel();
        }
    }

    void nextLevel()
    {
        player.position = new Vector3(player.position.x, player.position.y, 0);
        level++;
        levelText.text = "Level: " + level.ToString();
        spawnManager.IncreaseSpawnChance();
        if(level % 5 == 0)
        {
            spawnManager.DecreaseSpawnInterval();
        }
        DestroyAllObstacles();
    }

    void DestroyAllObstacles()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Obstacle");
        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(deathDelay);
        SceneManager.LoadScene("GameOver");
    }
}
