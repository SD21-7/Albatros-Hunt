using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PowerUpSpawner : MonoBehaviour
{
    EnemySpawner enemySpawner;

    [Range(0, 6f)] private float spawnTimer;
    [Range(0, 6f)] private float spawnRange;

    private int spawnAmount = 1;
    private int totalPowerUps;
    private int spawnedPowerUps;
    private float timer;
    private bool pauseSpawns;
    [SerializeField] GameObject PowerUpParent;

    [SerializeField] private List<GameObject> prefabs;
    [SerializeField] private Transform SpawnPosition;

    [Header("Normal Mode")] [SerializeField]
    private List<GameObject> normalPrefabs;

    [SerializeField] [Range(0, 10f)] private float normalSpawnTimer = 3;
    [SerializeField] private int normalTotalPowerUps = 1;

    [Header("Hard Mode")] [SerializeField] private List<GameObject> hardPrefabs;
    [SerializeField] [Range(0, 10f)] private float hardSpawnTimer = 2;
    [SerializeField] private int hardTotalPowerUps = 3;


    [Header("Extreme Mode")] [SerializeField]
    private List<GameObject> extremePrefabs;

    [SerializeField] [Range(0, 10f)] private float extremeSpawnTimer = 1;
    [SerializeField] private int extremeTotalPowerUps = 6;

    private void Start()
    {
        enemySpawner = GetComponentInParent<EnemySpawner>();
    }

    private void Update()
    {
        // Debug.Log("");
        // Debug.Log("PowerUpDiffNew" + enemySpawner.newDifficulty);
        // Debug.Log(timer);
        // if (enemySpawner.newDifficulty)
        // {
        //     setDifficulty(enemySpawner.difficulty);
        // }

        if (spawnedPowerUps < totalPowerUps)
        {
            timer += Time.deltaTime;
        }

        if (timer >= spawnTimer) SpawnPowerUps();
    }

    private void SpawnPowerUps()
    {
        if (pauseSpawns) return;
        if (spawnedPowerUps < totalPowerUps)
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                Vector3 SpawnOffset = new(UnityEngine.Random.Range(-spawnRange, spawnRange), 0, 0);
                GameObject spawned = Instantiate(prefabs[new Random().Next(prefabs.Count)]);
                spawned.transform.position = SpawnPosition.position + SpawnOffset;
                spawned.transform.parent = PowerUpParent.transform;
                spawnedPowerUps++;
            }
        }

        timer = 0;
    }

    public void setDifficulty(int difficulty)
    {
        spawnedPowerUps = 0;
        switch (difficulty)
        {
            case 0:
                spawnTimer = normalSpawnTimer;
                prefabs = normalPrefabs;
                totalPowerUps = normalTotalPowerUps;
                break;
            case 1:
                spawnTimer = hardSpawnTimer;
                prefabs = hardPrefabs;
                totalPowerUps = hardTotalPowerUps;
                break;
            case 2:
                spawnTimer = extremeSpawnTimer;
                prefabs = extremePrefabs;
                totalPowerUps = extremeTotalPowerUps;
                break;
        }
    }
}