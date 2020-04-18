using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    public int taste;
    public int food;
    public int health;

    public float timeToEat;

    private float timeSpawned;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSpawned += Time.deltaTime;
    }

    public void ApplyEaten(SheepHealth eater)
    {
        eater.Fullnes += food;
        eater.Health += health;
    }

    public virtual void OnSpawn()
    {
        Debug.Log("DAAAAMN");
        return;
    }
}
