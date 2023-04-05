using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using Random = System.Random;

public class PowerUpSpawner : MonoBehaviour
{
    EnemySpawner enemySpawner;

    [Range(0, 6f)] private float spawnTimer;
    [Range(0, 6f)] private float spawnRange;

    private int spawnAmount;
    private int totalPowerUps;
    private int spawnedPowerUps;
    private int difficulty = 0;
    private float timer;
    private bool pauseSpawns;

    [SerializeField] private List<GameObject> prefabs;
    [SerializeField] private Transform SpawnPosition;

    [Header("Normal Mode")] [SerializeField]
    private List<GameObject> normalPrefabs;

    [SerializeField] [Range(0, 10f)] private float normalSpawnTimer = 3;
    [SerializeField] private int normalSpawnAmount = 1;
    [SerializeField] private int normalTotalPowerUps = 1;

    [Header("Hard Mode")] [SerializeField] private List<GameObject> hardPrefabs;
    [SerializeField] [Range(0, 10f)] private float hardSpawnTimer = 2;
    [SerializeField] private int hardSpawnAmount = 1;
    [SerializeField] private int hardTotalPowerUps = 3;


    [Header("Extreme Mode")] [SerializeField]
    private List<GameObject> extremePrefabs;

    [SerializeField] [Range(0, 10f)] private float extremeSpawnTimer = 1;
    [SerializeField] private int extremeSpawnAmount = 1;
    [SerializeField] private int extremeTotalPowerUps = 6;

    private void Start()
    {
        enemySpawner = GetComponentInParent<EnemySpawner>();
    }

    private void Update()
    {
        // if (enemySpawner.newDifficulty)
        // {
        difficulty = enemySpawner.difficulty;
        setDifficulty(difficulty);
        // }
        timer += Time.deltaTime;
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
                spawnedPowerUps++;
            }
        }
        timer = 0;
    }

    public void setDifficulty(int difficulty)
    {
        switch (difficulty)
        {
            case 0:
                spawnTimer = normalSpawnTimer;
                spawnAmount = normalSpawnAmount;
                prefabs = normalPrefabs;
                totalPowerUps = normalTotalPowerUps;
                break;
            case 1:
                spawnTimer = hardSpawnTimer;
                spawnAmount = hardSpawnAmount;
                prefabs = hardPrefabs;
                totalPowerUps = hardTotalPowerUps;
                break;
            case 2:
                spawnTimer = extremeSpawnTimer;
                spawnAmount = extremeSpawnAmount;
                prefabs = extremePrefabs;
                totalPowerUps = extremeTotalPowerUps;
                break;
        }
    }
}