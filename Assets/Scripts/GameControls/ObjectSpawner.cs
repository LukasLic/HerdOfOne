//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    //private Random r;
    private bool spawning = false;
    public Transform spawnedObjectsParent;

    [SerializeField]
    public List<SpawnWave> early_waves;
    [SerializeField]
    public List<SpawnWave> mid_waves;
    [SerializeField]
    public List<SpawnWave> late_waves;

    private float spawnTimer = 0f;

    public void StartSpawning()
    {
        spawning = true;
    }

    public void StopSpawning()
    {
        StopAllCoroutines();
        spawning = false;
    }

    public void DeleteAllObjects()
    {
        //while(spawnedObjectsParent.childCount >= 1) // OOOPS
        //{
        //    Destroy(spawnedObjectsParent.GetChild(0).gameObject);
        //}

        for (int i = 0; i < spawnedObjectsParent.childCount; i++)
        {
            Destroy(spawnedObjectsParent.GetChild(i).gameObject);
        }
    }

    private void Start()
    {
        //r = new Random();
    }

    void Update()
    {
        if (!spawning)
            return;

        spawnTimer += Time.deltaTime;

        IterateThroughWaves(early_waves);
        IterateThroughWaves(mid_waves);
        IterateThroughWaves(late_waves);
    }

    

    private void IterateThroughWaves(List<SpawnWave> waves)
    {
        foreach (var wave in waves)
        {
            if (!wave.SpawnDepleted && spawnTimer >= wave.NextSpawnTime)
            {
                wave.WasSpawned();

                if (wave.spawnInsideSpawnArea)
                    SpawnInArea(wave.prefab);
                else if (!wave.spawnInsideSpawnArea)
                    SpawnOnEdge(wave.prefab);
                else
                    Debug.LogWarning($"Wave behaviour for {wave.name} not implemented!");

            }
        }
    }

    #region SpawnMethods

    private void SpawnInArea(GameObject prefab)
    {
        Instantiate(
            prefab,
            RandomPositionInRadius(GameManager.SpawnRadius),
            Quaternion.identity,
            spawnedObjectsParent
            );

        Debug.Log($"{prefab.name} spawned.");
    }

    private void SpawnOnEdge(GameObject prefab)
    {
        Instantiate(
            prefab,
            RandomPositionOnCircle(GameManager.GameAreaRadius),
            Quaternion.identity,
            spawnedObjectsParent
            );

        Debug.Log($"{prefab.name} spawned.");
    }

    #endregion

    private Vector3 RandomPositionInRadius(float radius)
    {
        var x = Random.value - 0.5f;  // -0,5f ... 0,5f
        var z = Random.value - 0.5f;  // -0,5f ... 0,5f
        return new Vector3(x, 0f, z).normalized * (radius * Random.value);
    }

    private Vector3 RandomPositionOnCircle(float radius)
    {
        var angle = Random.value * Mathf.PI * 2f;
        var x = Mathf.Cos(angle) * radius;
        var z = Mathf.Sin(angle) * radius;
        return new Vector3(x, 0f, z);
    }
}
