using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

[Serializable]
public class SpawnWave
{
    public string name;
    public GameObject prefab;
    public int amount;
    private int spawnedAmount = 0;

    [HideInInspector]
    public bool SpawnDepleted
    {
        get
        {
            return (amount - spawnedAmount) <= 0;
        }
    }

    [SerializeField] private float startTime = 0f;
    private float nextSpawnDelay = 0f;
    public float delayBetweenSpawns;
    public float NextSpawnTime
    {
        get
        {
            return startTime + nextSpawnDelay;
        }
    }

    public bool spawnInsideSpawnArea;
    public bool lookAtCenter;
    public bool lookAtSheep;

    public void WasSpawned()
    {
        spawnedAmount++;
        nextSpawnDelay += delayBetweenSpawns;
    }
}

