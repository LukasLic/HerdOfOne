using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprout : MonoBehaviour
{
    public BushType bushType;
    public float amount;

    private float timer;
    public float timeToDespawn;

    private BushSpawner spawner;

    private void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("GameController").GetComponent<BushSpawner>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= timeToDespawn)
        {
            StartDestroySprout();
        }
    }

    public void OnMouseDown()
    {
        Debug.Log($"Sprout {gameObject.name} was picked up.");
        spawner.SpeedUpRefreshing(amount, bushType);
        StartDestroySprout();
    }

    private void StartDestroySprout()
    {
        // Animation to disable collider and stuff.
        // Animation than calls DestroySprout(); 
        DestroySprout();
    }

    private void DestroySprout()
    {
        Destroy(gameObject);
    }
}
