using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawn : MonoBehaviour
{
    public GameObject target1Prefab;
    public GameObject target2Prefab;
    public GameObject target3Prefab;
    public GameObject parent;
    [Range(0,10f)]public float spawnRate;
    public int MaxSpawnCount;
    public int CurrentSpawnCount;
    
    // Start is called before the first frame update
    void Start()
    {
     InvokeRepeating("SpawnTarget", 0f, spawnRate);   
    }
    
    void SpawnTarget()
    {
        int randomTarget = Random.Range(1, 4);
        Vector3 spawnPosition = new Vector3(Random.Range(-8f, 2.5f), -5.5f, 0);
        switch (randomTarget)
        {
            case 1:
                Instantiate(target1Prefab, spawnPosition, Quaternion.identity).transform.parent = parent.transform;
                break;
            case 2:
                Instantiate(target2Prefab, spawnPosition, Quaternion.identity).transform.parent = parent.transform;
                break;
            case 3:
                Instantiate(target3Prefab, spawnPosition, Quaternion.identity).transform.parent = parent.transform;
                break;
        }
        CurrentSpawnCount++;
        if (CurrentSpawnCount >= MaxSpawnCount)
        {
            CancelInvoke("SpawnTarget");
            return;
        }
    }
}
