using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Bush> bushes;
    public static float gameArea = 20f;

    // Start is called before the first frame update
    void Start()
    {
        bushes = new List<Bush>();

        // Add initial bushes
        foreach (var bush in GameObject.FindGameObjectsWithTag("Bush"))
        {
            var component = bush.GetComponent<Bush>();
            if (component != null)
                bushes.Add(component);
        }
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    public Bush GetClosestBush(Vector3 position)
    {
        var distance = float.MaxValue;
        Bush closestBush = null;

        foreach (var bush in bushes)
        {
            if(bush != null)
            {
                var distFromPosition = (bush.transform.position - position).magnitude;
                if (distFromPosition < distance)
                {
                    distance = distFromPosition;
                    closestBush = bush;
                }
            }
        }

        return closestBush;
    }

    public void RemoveBush(Bush bush)
    {
        bushes.RemoveAll(b => b == null);

        bushes.Remove(bush);
        Destroy(bush.gameObject);
    }

    public void AddBush(GameObject bushObject)
    {
        var bush = bushObject.GetComponent<Bush>();
        if (bush != null)
        {
            bush.OnSpawn();
            bushes.Add(bush);
        }
    }

    public Bush GetBestBush(Vector3 position)
    {
        bushes.RemoveAll(b => b == null);

        var distance = float.MaxValue;
        var taste = int.MinValue;
        Bush closestBush = null;

        foreach (var bush in bushes)
        {
            if (bush.taste > taste)
            {
                distance = (bush.transform.position - position).magnitude;
                taste = bush.taste;
                closestBush = bush;
            }
            else if (bush.taste == taste)
            {
                var distFromPosition = (bush.transform.position - position).magnitude;
                if (distFromPosition < distance)
                {
                    distance = distFromPosition;
                    closestBush = bush;
                    taste = bush.taste;
                }
            }
        }

        return closestBush;
    }

    public static bool IsGameObjectTag(string tag)
    {
        switch (tag)
        {
            case "Sheep":
            case "Bush":
            case "Hunter":
            case "Car":
            case "Wolf":
                return true;
            default:
                return false;
        }
    }

    public static float GetDistance2D(Vector3 a, Vector3 b)
    {
        var dist = b - a;
        dist.y = 0f;
        return dist.magnitude;
    }

    public static int GetlayerId(int bit)
    {
        return (int)Mathf.Log(bit, 2f);
    }
}
