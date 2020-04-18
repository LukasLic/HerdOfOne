using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    private bool spawning = false;
    public Transform spawnedObjectsParent;

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
}
