using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotator : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.Rotate(transform.up, speed * Time.deltaTime);
    }
}
