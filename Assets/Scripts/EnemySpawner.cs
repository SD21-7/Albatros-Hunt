using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using Random = System.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs;
    [Range(0,10f)]private float spawnTimer;
    [Range(0,6f)]private float spawnRange;
    private int spawnAmount;
    private int totalEnemies;
    [SerializeField] private Transform SpawnPosition; 
    private float timer;
    private int spawnedEnemies;
    private int difficulty = 0;
    private bool pauseSpawns;
    
    [SerializeField] [Range(0,10f)]private float normalSpawnTimer = 3;
    [SerializeField] private int normalSpawnAmount = 1;
    [SerializeField] private int normalTotalEnemies = 10;
    
    [SerializeField] [Range(0,10f)]private float hardSpawnTimer = 2;
    [SerializeField] private int hardSpawnAmount = 2;
    [SerializeField] private int hardTotalEnemies = 20;
    
    [SerializeField] [Range(0,10f)]private float extremeSpawnTimer = 1;
    [SerializeField] private int extremeSpawnAmount = 2;
    [SerializeField] private int extremeTotalEnemies = 25;

    private void Start()
    {
        setDifficulty(difficulty);
    }

    private void Update()
    {
        if(timer > 0) timer -= Time.deltaTime;
        else SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        if(pauseSpawns) return;

        timer = spawnTimer;
        for (int i = 0; i < spawnAmount; i++) {
            Vector3 SpawnOffset = new(UnityEngine.Random.Range(-spawnRange, spawnRange), 0, 0);
            GameObject spawned = Instantiate(prefabs[new Random().Next(prefabs.Count)]);
            spawned.transform.position = SpawnPosition.position + SpawnOffset;
            spawnedEnemies++;
        }
        
        if (spawnedEnemies >= totalEnemies) {
            levelEnd();
        }
        
    }

    private void levelEnd()
    {
        Debug.Log("Next difficulty");
        setDifficulty(++difficulty);
    }

    private void setDifficulty(int diff)
    {
        spawnedEnemies = 0;
        switch (diff) {
            case 1: {
                spawnTimer = hardSpawnTimer;
                spawnAmount = hardSpawnAmount;
                totalEnemies = hardTotalEnemies;
                break;
            }
            case 2: {
                spawnTimer = extremeSpawnTimer;
                spawnAmount = extremeSpawnAmount;
                totalEnemies = extremeTotalEnemies;
                break;
            }
            default: {
                spawnTimer = normalSpawnTimer;
                spawnAmount = normalSpawnAmount;
                totalEnemies = normalTotalEnemies;
                break;
            }
        }
    }
}

