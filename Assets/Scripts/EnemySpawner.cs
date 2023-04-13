using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class EnemySpawner : MonoBehaviour
{
    public PowerUpSpawner powerUpSpawner;
    public ScoreController scoreController;
    
    [Range(0, 10f)] private float spawnTimer;
    [Range(0, 6f)] private float spawnRange;

    private int spawnAmount;
    private int totalEnemies;
    private int spawnedEnemies;
    public int difficulty = 0;
    public bool newDifficulty = true;
    private float timer;
    public bool pauseSpawns;
    [SerializeField] GameObject TargetParent;

    [SerializeField] private List<GameObject> prefabs;
    [SerializeField] private Transform SpawnPosition;

    [Header("Normal Mode")]
    [SerializeField] [Range(0, 10f)] private float normalSpawnTimer = 3;
    [SerializeField] private int normalSpawnAmount = 1;
    [SerializeField] private int normalTotalEnemies = 10;

    [Header("Hard Mode")] 
    [SerializeField] [Range(0, 10f)] private float hardSpawnTimer = 2;
    [SerializeField] private int hardSpawnAmount = 2;
    [SerializeField] private int hardTotalEnemies = 20;

    [Header("Extreme Mode")]
    [SerializeField] [Range(0, 10f)]private float extremeSpawnTimer = 1;
    [SerializeField] private int extremeSpawnAmount = 2;
    [SerializeField] private int extremeTotalEnemies = 25;

    private void Start()
    {
        scoreController = GameObject.Find("GameInit").GetComponent<ScoreController>();
        powerUpSpawner = GetComponentInParent<PowerUpSpawner>();
        
        newDifficulty = true;
        if (newDifficulty)
        {
            setDifficulty(difficulty);
            powerUpSpawner.setDifficulty(difficulty);
        }
    }

    private void Update()
    {
        // Debug.Log("EnemyDiffNew" + newDifficulty);
        if (timer > 0) timer -= Time.deltaTime;
        else SpawnEnemies();

        if (GameObject.FindGameObjectWithTag("Target") == null && difficulty != 2)
        {
            newDifficulty = true;
            setDifficulty(++difficulty);
            powerUpSpawner.setDifficulty(difficulty);
        }
        
        if (difficulty == 2 && GameObject.FindGameObjectWithTag("Target") == null && spawnedEnemies >= totalEnemies)
        {
            scoreController.SaveScore();
            SceneManager.LoadScene("End");
        }
    }

    private void SpawnEnemies()
    {
        if (pauseSpawns) return;

        timer = spawnTimer;
        for (int i = 0; i < spawnAmount; i++)
        {
            Vector3 SpawnOffset = new(UnityEngine.Random.Range(-spawnRange, spawnRange), 0, 0);
            GameObject spawned = Instantiate(prefabs[new Random().Next(prefabs.Count)]);
            spawned.transform.position = SpawnPosition.position + SpawnOffset;
            spawned.transform.parent = TargetParent.transform;
            spawnedEnemies++;
        }

        if (spawnedEnemies >= totalEnemies)
        {
            pauseSpawns = true;
        }
    }

    private void setDifficulty(int diff)
    {
        spawnedEnemies = 0;
        switch (diff)
        {
            case 1:
            {
                spawnTimer = hardSpawnTimer;
                spawnAmount = hardSpawnAmount;
                totalEnemies = hardTotalEnemies;
                pauseSpawns = false;
                break;
            }
            case 2:
            {
                spawnTimer = extremeSpawnTimer;
                spawnAmount = extremeSpawnAmount;
                totalEnemies = extremeTotalEnemies;
                pauseSpawns = false;
                break;
            }
            default:
            {
                spawnTimer = normalSpawnTimer;
                spawnAmount = normalSpawnAmount;
                totalEnemies = normalTotalEnemies;
                pauseSpawns = false;
                // newDifficulty = false;
                break;
            }
        }
        newDifficulty = false;
    }
}