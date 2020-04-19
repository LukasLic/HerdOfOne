using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehaviour : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    private BoxCollider boxCollider;

    private Transform sheep;

    private bool crashed;

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
        sheep = GameObject.FindGameObjectWithTag("Sheep").transform;
        boxCollider = GetComponent<BoxCollider>();
        crashed = false;
        transform.LookAt(sheep.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
        
        if(!crashed)
        {
            //transform.rotation =
            //    Quaternion.RotateTowards(
            //        transform.rotation,
            //        sheep.rotation,
            //        rotationSpeed * Time.deltaTime
            //        );

            //Vector3 direction = Vector3.RotateTowards(transform.forward, sheep.position, 1f, 1f);
            //Quaternion toRotation = Quaternion.LookRotation(direction, transform.up);
            //transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.time);
        }

        // Destroy if out of the game area
        if (GameManager.GetDistance2D(transform.position, Vector3.zero) > GameManager.GameAreaRadius)
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
            transform.LookAt(transform.position - transform.forward);
            crashed = true;
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
            //Debug.LogWarning($"Collision behaviour with {other.name} not set!");
        }
    }
}
