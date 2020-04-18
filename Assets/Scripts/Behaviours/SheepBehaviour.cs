using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepBehaviour : MonoBehaviour
{
    public GameManager manager;
    private SheepHealth health;

    public float speed = 1f;
    public float stoppingDistance = 0.4f;

    private Bush targetBush;
    private bool eating = false;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<SheepHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if(targetBush == null)
        {
            FindNewBush();
        }

        if(targetBush != null && !eating)
        {
            if(Arrived())   // Start eating bush.
            {
                eating = true;
                StartCoroutine(EatBush(targetBush.timeToEat));
            }
            else    // Move towards the bush.
            {
                var direction = (targetBush.transform.position - transform.position).normalized;
                var travel = direction * speed * Time.deltaTime;
                travel.y = 0f;
                transform.position += travel;
            }
        }
    }

    private void FindNewBush()
    {
        var target = manager.GetBestBush(transform.position);

        if (target != null)
        {
            targetBush = target;
        }
    }

    public void ForgetBush()
    {
        if(!eating)
        {
            FindNewBush();
        }
    }

    public void DeliciousBushAppeared()
    {
        if (targetBush.GetType() != typeof(DeliciousBush)
            ||
            (targetBush.GetType() == typeof(DeliciousBush) && !eating))
        {
            targetBush = null;
            eating = false;
            StopAllCoroutines();
        }
    }

    /// <summary>
    /// Returns true if distance to targetBush is less or equal to stoppingDistance.
    /// </summary>
    private bool Arrived()
    {
        return GameManager.
            GetDistance2D(targetBush.transform.position, transform.position)
            <=
            stoppingDistance;
    }

    public void BushGotDestroyed(int destroyedBushId)
    {
        if (eating 
            &&
            targetBush.gameObject.GetInstanceID() == destroyedBushId)
        {
            Debug.Log($"Sheep was interrupted eating.");
            targetBush = null;
            eating = false;
            StopAllCoroutines();
        }
    }

    public IEnumerator EatBush(float time)
    {
        Debug.Log($"Sheep started eating {targetBush.name}");
        yield return new WaitForSeconds(time);
        FinishEating();

        yield return null;
    }

    private void FinishEating()
    {
        // This MUST be first!
        eating = false;

        // Leave this here, important for running when bush got destroyed while eating!
        if (targetBush != null)
        {
            Debug.Log($"Sheep finished eating {targetBush.name}");

            targetBush.ApplyEaten(health);
            manager.RemoveBush(targetBush);
        }

        targetBush = null;
    }
}
