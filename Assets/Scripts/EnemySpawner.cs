using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs;
    [SerializeField] [Range(0,10f)]private float spawnTimer;
    [SerializeField] private int spawnAmount;
    [SerializeField] private int totalEnemies;
    private float timer;
    private int spawnedEnemies;

    private void Start()
    {
        timer = spawnTimer;
    }

    private void Update()
    {
        if(timer > 0) timer -= Time.deltaTime;
        else
        {
            timer = spawnTimer;
            for (int i = 0; i < spawnAmount; i++)
            {
                Vector3 SpawnPosition = new Vector3(UnityEngine.Random.Range(-8f, 2.5f), -5.5f, 0);
                GameObject spawned = Instantiate(prefabs[new Random().Next(prefabs.Count)]);
                spawned.transform.position = SpawnPosition;
                spawnedEnemies++;
            }
        }
    }
}

