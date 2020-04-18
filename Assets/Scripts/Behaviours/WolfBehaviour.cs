using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfBehaviour : MonoBehaviour
{
    public float speed;
    public int attackDamage;
    private SheepHealth sheep;

    private bool runAway = false;
    Vector3 runDirection;

    // Start is called before the first frame update
    void Start()
    {
        sheep = GameObject.FindGameObjectWithTag("Sheep").GetComponent<SheepHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        // get direction to move.
        Vector3 direction;
        if(!runAway)
        {
            direction = (sheep.transform.position - transform.position).normalized;
        }
        else
        {
            direction = runDirection;
        }

        // Move.
        transform.position += direction * Time.deltaTime * speed;

        // Destroy if out of the game area.
        if (GameManager.GetDistance2D(transform.position, Vector3.zero) > GameManager.gameArea)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Wolf collided with: {other.name}");

        if (other.tag == "Sheep")
        {
            // Bite the sheep and run away from map.
            if(!runAway)
            {
                sheep.Health -= attackDamage;
                runAway = true;

                // Find closest exit
                runDirection = (transform.position - Vector3.zero).normalized;
            }
        }
        else if (other.tag == "Car")
        {
            // Just die :D
            // Will be destroyed by car.
        }
        else if (other.tag == "Bush")
        {
            // Get the bush component.
            var component = other.GetComponent<Bush>();

            if(component.GetType() == typeof(ThornyBush))
            {
                // Run in opposite direction.
                runDirection = (transform.position - other.transform.position).normalized;
                runAway = true;

                Destroy(other.gameObject);
            }
        }
        else if (GameManager.IsGameObjectTag(other.tag))
        {
            //Debug.LogWarning($"Collision behaviour with {other.name} not set!");
        }
    }
}
