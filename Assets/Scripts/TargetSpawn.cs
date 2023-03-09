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
    
    [Header("Spawn Limit")]
    public int MaxSpawnCount;
    public int Target1Max;
    public int Target2Max;
    public int Target3Max;
    
    private int CurrentSpawnCount;
    private int Target1Count;
    private int Target2Count;
    private int Target3Count;
    
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
                if (Target1Count < Target1Max)
                {
                    Instantiate(target1Prefab, spawnPosition, Quaternion.identity).transform.parent = parent.transform;
                    Target1Count++;
                    CurrentSpawnCount++;
                }
                else
                {
                    SpawnTarget();
                }
                break;
            case 2:
                if (Target2Count < Target2Max)
                {
                    Instantiate(target2Prefab, spawnPosition, Quaternion.identity).transform.parent = parent.transform;
                    Target2Count++;
                    CurrentSpawnCount++;
                }
                else
                {
                    SpawnTarget();
                }
                break;
            case 3:
                if (Target3Count < Target3Max)
                {
                    Instantiate(target3Prefab, spawnPosition, Quaternion.identity).transform.parent = parent.transform;
                    Target3Count++;
                    CurrentSpawnCount++;
                }
                else
                {
                    SpawnTarget();
                }
                break;
        }
        if (CurrentSpawnCount >= MaxSpawnCount)
        {
            CancelInvoke("SpawnTarget");
            return;
        }
    }
}
