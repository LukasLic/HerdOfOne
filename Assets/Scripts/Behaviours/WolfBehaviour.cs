using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfBehaviour : MonoBehaviour
{
    public float speed;

    public float dashDistance;
    public float dashSpeed;

    private float actualSpeed;

    public int attackDamage;
    private SheepHealth sheep;

    private bool runAway = false;
    Vector3 runDirection;

    // Start is called before the first frame update
    void Start()
    {
        actualSpeed = speed;
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
        direction.y = 0f;
        transform.LookAt(transform.position + direction);
        transform.position += direction * Time.deltaTime * actualSpeed;

        if (!runAway && (transform.position - sheep.transform.position).magnitude <= dashDistance)
            actualSpeed = dashSpeed;

        // Destroy if out of the game area.
        if (GameManager.GetDistance2D(transform.position, Vector3.zero) > GameManager.GameAreaRadius)
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
                actualSpeed = dashSpeed;

                GameObject.FindGameObjectWithTag("GameController").
                    GetComponent<GameManager>()
                    .RemoveBush(component);
                //Destroy(other.gameObject);
            }
        }
        else if (GameManager.IsGameObjectTag(other.tag))
        {
            //Debug.LogWarning($"Collision behaviour with {other.name} not set!");
        }
    }
}
