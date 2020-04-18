using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<Bush> bushes;
    public const float gameArea = 20f;

    public GameUIController uIController;
    public SheepBehaviour sheep;

    private float maxGameLength;
    private float _gameTimer;
    private float GameTimer
    {
        get => _gameTimer;
        set
        {
            _gameTimer = value;
            uIController.SetTime(GameTimer, maxGameLength);

            if (GameTimer >= maxGameLength)
            {
                Debug.LogError("WIN");
            }
        }
    }
    public float initialGameLength;

    // Start is called before the first frame update
    void Start()
    {
        maxGameLength = initialGameLength;
        GameTimer = 0f;

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
        GameTimer += Time.deltaTime;
    }

    #region Game states
    public void GameOver()
    {

    }

    public void Victory()
    {

    }

    // Removes every object from the game.
    // Useful when you don't want player to get killed while reading Victory screen.
    private void CleanGame()
    {

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

        var distance = float.MaxValue;
        var taste = int.MinValue;
        Bush closestBush = null;

        // Loops finds the best tasting and then closest bush.
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
