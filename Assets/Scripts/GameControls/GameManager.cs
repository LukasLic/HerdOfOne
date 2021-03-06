﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool gameOver;

    private List<Bush> bushes;
    public const float GameAreaRadius = 15f;
    public const float SpawnRadius = 10f;

    public GameUIController uiController;
    public SheepBehaviour sheep;

    private ObjectSpawner spawner;

    private float maxGameLength;
    private float _gameTimer;
    private float GameTimer
    {
        get => _gameTimer;
        set
        {
            _gameTimer = value;
            uiController.SetTime(GameTimer, maxGameLength);

            if (GameTimer >= maxGameLength)
            {
                Victory();
            }
        }
    }
    public float initialGameLength;

    // Start is called before the first frame update
    void Start()
    {
        maxGameLength = initialGameLength;
        GameTimer = 0f;
        gameOver = false;

        bushes = new List<Bush>();

        // Add initial bushes
        foreach (var bush in GameObject.FindGameObjectsWithTag("Bush"))
        {
            var component = bush.GetComponent<Bush>();
            if (component != null)
                bushes.Add(component);
        }

        spawner = GetComponent<ObjectSpawner>();
        spawner.StartSpawning();
    }

    // Update is called once per frame
    void Update()
    {
        GameTimer += Time.deltaTime;
    }

    #region Game states
    public void GameOver()
    {
        gameOver = true;
        CleanGame();
        uiController.GameOver();
    }

    public void Victory()
    {
        if(!gameOver)
        {
            CleanGame();
            uiController.Victory();
        }
    }

    // Removes every object from the game.
    // Useful when you don't want player to get killed while reading Victory screen.
    private void CleanGame()
    {
        spawner.StopSpawning();
        spawner.DeleteAllObjects();

        GetComponent<BushSpawner>().enabled = false;

        if(sheep != null)
        {
            sheep.enabled = false;
            sheep.StopAllCoroutines();
            sheep.gameObject.GetComponent<SheepHealth>().enabled = false;
            sheep.gameObject.SetActive(false);
            Destroy(sheep.gameObject);
        }
    }
    #endregion

    #region Logic around bushes list
    public void RemoveBush(Bush bush)
    {
        bushes.RemoveAll(b => b == null);
        bushes.Remove(bush);
        bush.Despawn();

        sheep.BushGotDestroyed(bush.gameObject.GetInstanceID());
    }

    public void AddBush(GameObject bushObject)
    {
        var bush = bushObject.GetComponent<Bush>();
        if (bush != null)
        {
            bushes.Add(bush);
            bush.OnSpawn();

            sheep.ForgetBush();
        }
    }

    /// <summary>
    /// Returns best bush in the scene. Null if no bushes.
    /// </summary>
    public Bush GetBestBush(Vector3 position)
    {
        // Remove null values. Better be safe than sorry.
        bushes.RemoveAll(b => b == null);

        //var distance = float.MaxValue;
        var taste = float.MinValue;
        Bush closestBush = null;

        // Loops finds the best tasting and then closest bush.
        foreach (var bush in bushes)
        {
            var distance = (bush.transform.position - position).magnitude;
            var tasteValue = bush.taste / distance;

            if (tasteValue > taste)
            {
                //distance = (bush.transform.position - position).magnitude;
                taste = tasteValue;
                closestBush = bush;
            }
        }

        //Debug.Log(taste);
        // No bush is close enough.
        if (taste < 0.4f)
            return null;

        return closestBush;
    }
    #endregion

    #region Static

    // Returns true if the tag is used by custom game object.
    // PS: If something crashed you probably forgot to change this. Idiot...
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

    // Returns distance between two positions, ignoring y.
    public static float GetDistance2D(Vector3 a, Vector3 b)
    {
        var dist = b - a;
        dist.y = 0f;
        return dist.magnitude;
    }

    // Returns layer id in editor view format.
    public static int GetlayerId(int bit)
    {
        return (int)Mathf.Log(bit, 2f);
    }

    #endregion

    #region Depracted
    // DEPRACTED
    //public Bush GetClosestBush(Vector3 position)
    //{
    //    bushes.RemoveAll(b => b == null);

    //    var distance = float.MaxValue;
    //    Bush closestBush = null;

    //    foreach (var bush in bushes)
    //    {
    //        // Condition for errors in the list. Better be safe than sorry.
    //        if (bush != null)
    //        {
    //            var distFromPosition = (bush.transform.position - position).magnitude;
    //            if (distFromPosition < distance)
    //            {
    //                distance = distFromPosition;
    //                closestBush = bush;
    //            }
    //        }
    //    }

    //    return closestBush;
    //}
    #endregion
}
