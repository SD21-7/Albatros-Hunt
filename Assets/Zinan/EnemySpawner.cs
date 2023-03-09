using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabs;
    [SerializeField] private float spawnTimer;
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
                GameObject spawned = Instantiate(prefabs[new Random().Next(prefabs.Count)]);
                spawned.SendMessage("RandomiseValues");
                spawnedEnemies++;
            }
        }
    }
}

