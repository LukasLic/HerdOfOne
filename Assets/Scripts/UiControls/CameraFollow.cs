using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Transform cameraChild;

    public float speed;
    private Vector3 offset;

    private void Start()
    {
        offset = target.position + transform.position;
    }

    void LateUpdate()
    {
        if (target == null)
        {
            this.enabled = false;
            return;
        }

        var targetPos = target.position + offset;
        var smoothSpeed = speed * Time.deltaTime;

        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed);

        // Looking

        //// A
        //var targetLookPosition = transform.position;
        //targetLookPosition.z += transform.position.z - target.position.z;
        //targetLookPosition.y = 0f;

        //// B
        //var targetLookPosition = target.position;
        //targetLookPosition.x = transform.position.x;
        //Debug.DrawLine(cameraChild.position, targetLookPosition, Color.blue);
        //cameraChild.LookAt(targetLookPosition);
    }
}
