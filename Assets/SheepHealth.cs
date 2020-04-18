using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepHealth : MonoBehaviour
{
    public float initialHealth = 100f;
    public float initialFullnes = 100f;

    private float maxHealth;
    private float _health;
    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if (_health > maxHealth)
                _health = maxHealth;
            else if (_health < 0f)
            {
                Debug.LogError("GAME OVER");
            }
        }
    }

    private float maxFullnes;
    private float _fullnes;
    public float Fullnes
    {
        get
        {
            return _fullnes;
        }
        set
        {
            _fullnes = value;
            if (_fullnes > maxFullnes)
                _fullnes = maxFullnes;
            else if (_fullnes < 0f)
            {
                Debug.LogError("GAME OVER");
            }
        }
    }

    public float hungerMultiplier = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = initialHealth;
        maxFullnes = initialFullnes;

        Health = initialHealth;
        Fullnes = initialFullnes;
    }

    // Update is called once per frame
    void Update()
    {
        Fullnes -= Time.deltaTime * hungerMultiplier;
    }


}
