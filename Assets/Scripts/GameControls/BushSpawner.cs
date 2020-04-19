using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushSpawner : MonoBehaviour
{
    public LayerMask groundMask;

    // Prefabs
    public List<BushSpawnGroup> spawnGroups;
    //public BushSpawnGroup defaultBush;
    //public BushSpawnGroup bigBush;
    //public BushSpawnGroup thornyBush;
    //public BushSpawnGroup deliciousBush;

    public Transform bushesParent;
    public GameManager manager;

    private void Start()
    {
        foreach (var group in spawnGroups)
        {
            //group.Activate();
            group.Deactivate();
        }
    }

    void Update()
    {
        bool placedBush = false;

        // Set update time for bushes groups.
        // Check user input to spawn bush.
        foreach (var group in spawnGroups)
        {
            group.FrameDeltaTime = Time.deltaTime;

            if (!placedBush && Input.GetKeyDown(group.key) && group.IsAvaliable())
            {
                Debug.Log($"User pressed: {group.key}. Trying to spawn: {group.prefab.name}.");
                if(SpawnBush(group.prefab))
                {
                    placedBush = true;
                    group.Deactivate();
                }
                
            }
        }

        // GAMEPLAY DEPRACTED
        //// Try to root out thorny bush.
        //if (!placedBush && Input.GetButtonDown("Fire1"))
        //{
        //    RootOutThornyBush();
        //}
    }

    private void RootOutThornyBush()
    {
        // Shoot ray from the camera.
        var camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(camRay, out RaycastHit hit, float.MaxValue))
        {
            // Destroy thorny bush if it was hit.
            var component = hit.collider.GetComponent<ThornyBush>();

            if (component != null)
            {
                manager.RemoveBush(component);
            }
            else
                Debug.Log($"Incorrect root out: {hit.collider.name} ({hit.collider.tag}) was hit.");
        }
    }

    /// <summary>
    /// Spawns bush and returns true if spawning was succesful.
    /// </summary>
    private bool SpawnBush(GameObject prefab)
    {
        // Shoot ray from the camera.
        var camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(camRay, out RaycastHit hit, float.MaxValue))
        {
            // Did I hit groundMask succesfully?
            if (hit.collider.gameObject.layer == GameManager.GetlayerId(groundMask.value))
            {
                var bush = Instantiate(prefab, hit.point, Quaternion.identity, bushesParent);
                manager.AddBush(bush);
                return true;
            }
            else
            {
                Debug.Log($"Incorrect spawn: {hit.collider.name} ({hit.collider.gameObject.layer}) was hit.");
            }
        }

        return false;
    }

    public void SpeedUpRefreshing(float amount, BushType identifier)
    {
        foreach (var group in spawnGroups)
        {
            if(group.identifier == identifier)
            {
                group.FrameDeltaTime = amount;
                return;
            }
        }

        Debug.LogWarning($"Didn't find {identifier.ToString()} to refresh!");
    }
}
