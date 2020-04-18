using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushSpawner : MonoBehaviour
{
    public LayerMask groundMask;

    public GameObject bushPrefab;
    public GameObject thornyPrefab;
    public GameObject deliciousPrefab;

    public Transform bushesParent;
    public GameManager manager;

    void Update()
    {
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
        var camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(camRay, out RaycastHit hit, float.MaxValue))
        {
            Vector3 point = hit.point;
            if (hit.collider.tag == "Bush")
            {
                var component = hit.collider.GetComponent<ThornyBush>();
                if (component != null)
                    Destroy(hit.collider.gameObject);
            }
            else
            {
                Debug.Log($"Incorrect root out: {hit.collider.name} ({hit.collider.tag}) was hit.");
            }
        }
    }

    private void SpawnBush(GameObject prefab)
    {
        var camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(camRay, out RaycastHit hit, float.MaxValue))
        {
            Vector3 point = hit.point;
            if(hit.collider.gameObject.layer == GameManager.GetlayerId(groundMask.value))
            {
                var bush = Instantiate(prefab, hit.point, Quaternion.identity, bushesParent);
                manager.AddBush(bush);
            }
            else
            {
                Debug.Log($"Incorrect spawn: {hit.collider.name} ({hit.collider.gameObject.layer}) was hit.");
            }
        }

        //if (Physics.Raycast(camRay, out RaycastHit hit, float.MaxValue, groundMask))
        //{
        //    Vector3 point = hit.point;
        //    var bush = Instantiate(bushPrefab, hit.point, Quaternion.identity, bushesParent);
        //    manager.AddBush(bush);
        //}
    }
}
