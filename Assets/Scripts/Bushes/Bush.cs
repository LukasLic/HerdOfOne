using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    public int taste;
    public int food;
    public int health;
    public float timeToEat;

    public void ApplyEaten(SheepHealth eater)
    {
        eater.Fullnes += food;
        eater.Health += health;
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }

    public virtual void OnSpawn()
    {
        Debug.Log($"{gameObject.name} spawned on {transform.position}.");
    }
}
