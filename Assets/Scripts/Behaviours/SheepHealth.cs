using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepHealth : MonoBehaviour
{
    public float initialHealth = 100f;
    public float initialFullnes = 100f;

    public GameUIController uIController;
    public GameManager gameManager;

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
                uIController.SetHealth(_health, maxHealth);

                GameObject.
                    FindGameObjectWithTag("GameController").
                    GetComponent<GameManager>().
                    GameOver();

                gameManager.GameOver();
            }

            uIController.SetHealth(_health, maxHealth);
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
                uIController.SetFood(_fullnes, maxFullnes);

                GameObject.
                    FindGameObjectWithTag("GameController").
                    GetComponent<GameManager>().
                    GameOver();

                gameManager.GameOver();
            }

            uIController.SetFood(_fullnes, maxFullnes);
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
