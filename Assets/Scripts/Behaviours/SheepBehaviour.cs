using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepBehaviour : MonoBehaviour
{

    public GameManager manager;
    private SheepHealth health;

    public float speed = 1f;
    public float stoppingDistance = 0.4f;

    //private Vector3 targetDestination = Vector3.zero;
    private Bush targetBush;
    //private bool travelling = false;
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
            if(Arrived())
            {
                eating = true;
                StartCoroutine(EatBush(targetBush.timeToEat));
            }
            else
            {
                var direction = (targetBush.transform.position - transform.position).normalized;
                var travel = direction * speed * Time.deltaTime;
                travel.y = 0f;

                transform.position += travel;
            }
        }
        
        //if (travelling && Arrived())
        //{
        //    //Debug.Log("BEEEEE");
        //    travelling = false;
        //    StartCoroutine(EatBush(targetBush.timeToEat));
        //}

        //if (travelling)
        //{
        //    if(targetBush != null)
        //    {
        //        var direction = (targetBush.transform.position - transform.position).normalized;
        //        var travel = direction * speed * Time.deltaTime;
        //        travel.y = 0f;

        //        transform.position += travel;
        //    }
        //    else
        //    {
        //        travelling = false;
        //    }
        //}
        //else if (!travelling && targetBush == null)
        //{
        //    FindNewBush();   
        //}
    }

    private void FindNewBush()
    {
        var target = manager.GetBestBush(transform.position);
        if (target != null)
        {
            //targetDestination = target.transform.position;
            //targetDestination.y = 0f;
            targetBush = target;
            //travelling = true;
        }
    }

    public void ForgetBush()
    {
        if(!eating)
        {
            targetBush = null;
        }
    }

    private bool Arrived()
    {
        return GameManager.GetDistance2D(targetBush.transform.position, transform.position) <= stoppingDistance;
    }

    private IEnumerator EatBush(float time)
    {
        Debug.Log("Eating bush");
        yield return new WaitForSeconds(time);

        if(targetBush != null)
        {
            Debug.Log("Bush eaten");

            targetBush.ApplyEaten(health);
            manager.RemoveBush(targetBush);
        }

        targetBush = null;
        eating = false;

        yield return null;
    }
}
