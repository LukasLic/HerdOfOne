using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehaviour : MonoBehaviour
{
    public float speed;
    private BoxCollider boxCollider;

    private GameManager _manager;
    private GameManager Manager
    {
        get
        {
            if(_manager == null)
            {
                _manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
            }
            return _manager;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;

        // Destroy if out of the game area
        if (GameManager.GetDistance2D(transform.position, Vector3.zero) > GameManager.gameArea)
        {
            Destroy(boxCollider);
            Destroy(this.gameObject);
        }


    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Car collided with: {other.name}");

        if(other.tag == "Sheep")
        {
            other.GetComponent<SheepHealth>().Health -= float.MaxValue;
        }
        else if (other.tag == "Car")
        {
            
        }
        else if (other.tag == "Wolf")
        {
            Destroy(other.gameObject);
        }
        else if(other.tag == "Bush")
        {
            Manager.RemoveBush(other.GetComponent<Bush>());
            //other.transform.SetParent(this.transform);
        }
        else if(GameManager.IsGameObjectTag(other.tag))
        {
            Debug.LogWarning($"Collision behaviour with {other.name} not set!");
        }
    }
}
