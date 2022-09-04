using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstacle;
    public Transform player;
    private const int spawnDistance = 100;
    private const int stopSpawnDistance = 1350;
    private const int maxEnemies = 4;
    private const int spaceBetweenObstacles = 2;

    public int[] spawnChance = new int[5]; // [number of enemies]
    private int accumulatedProbability; // Sum of all values in spawnChance
    [SerializeField] float spawnInterval; // in seconds
    private float xRange = 6.0f;


    // Start is called before the first frame update
    void Start()
    {
        SetAcumulatedProbability();
        StartCoroutine(SpawnObstacle());
    }

    IEnumerator SpawnObstacle()
    {
        yield return new WaitForSeconds(spawnInterval);
        if(player.position.z < stopSpawnDistance)
        {
            int enemiesToSpawn = GetRandomObstacles();
            int spawnType = Random.Range(1, 3);
            if(spawnType == 1)
            {
                spawnTyp1(enemiesToSpawn);
            }
            else if(spawnType == 2)
            {
                spawnType2(enemiesToSpawn);
            }
        }
        StartCoroutine(SpawnObstacle());
    }

    void spawnTyp1(int enemiesToSpawn)
    {
        float xPos = Random.Range(-xRange, xRange - enemiesToSpawn);
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Vector3 spawnPos = player.position + new Vector3(xPos - player.position.x + i * spaceBetweenObstacles, 0, spawnDistance);
            Instantiate(obstacle, spawnPos, obstacle.transform.rotation);
        }
    }

    void spawnType2(int enemiesToSpawn)
    {
        float xPos = Random.Range(-xRange, xRange - enemiesToSpawn);
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Vector3 spawnPos = player.position + new Vector3(xPos - player.position.x + i * spaceBetweenObstacles, 0, spawnDistance + i * spaceBetweenObstacles);
            Instantiate(obstacle, spawnPos, obstacle.transform.rotation);
        }
    }

    private int GetRandomObstacles()
    {
        int randomNumber = Random.Range(0, accumulatedProbability);
        int addedChance = 0;
        for (int i = 0; i < spawnChance.Length; i++)
        {
            if (randomNumber < spawnChance[i] + addedChance)
            {
                return i + 1;
            }
            addedChance += spawnChance[i];
        }

        return -1; // Shouldn't reach this return
    }

    public void SetAcumulatedProbability()
    {
        int newProbability = 0;
        for (int i = 0; i < spawnChance.Length; i++)
        {
            newProbability += spawnChance[i];
        }
        accumulatedProbability = newProbability;
    }

    public void IncreaseSpawnChance()
    {
        spawnChance[0] += 1;
        spawnChance[1] += 2;
        spawnChance[2] += 3;
        spawnChance[3] += 4;
        spawnChance[4] += 5;
    }

    public void DecreaseSpawnInterval()
    {
        if(spawnInterval > 0.5f)
        {
            spawnInterval -= 0.05f;
        }
    }
}
