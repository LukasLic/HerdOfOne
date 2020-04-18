using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushSpawner : MonoBehaviour
{
    public LayerMask groundMask;

    // Prefabs
    public GameObject bushPrefab;
    public GameObject bigPrefab;
    public GameObject thornyPrefab;
    public GameObject deliciousPrefab;

    public Transform bushesParent;
    public GameManager manager;

    void Update()
    {
        // TODO
        if(Input.GetButtonDown("Fire1"))
        {
            
            if (Input.GetButton("Jump"))
            {
                SpawnBush(deliciousPrefab);
            }
            else
            {
                SpawnBush(bushPrefab);
            }
        }
        else if(Input.GetButtonDown("Fire2"))
        {
            if(Input.GetButton("Jump"))
            {
                RootOutThornyBush();
            }
            else
            {
                SpawnBush(thornyPrefab);
            }
        }
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

    private void SpawnBush(GameObject prefab)
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
            }
            else
            {
                Debug.Log($"Incorrect spawn: {hit.collider.name} ({hit.collider.gameObject.layer}) was hit.");
            }
        }
    }
}
