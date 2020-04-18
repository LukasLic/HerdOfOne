using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class BushSpawnGroup
{
    public BushType identifier;
    public GameObject prefab;
    public KeyCode key;

    private float _timer;
    private float Timer
    {
        get => _timer;
        set
        {
            _timer = value;
            refreshSlider.value = (timeToSpawn - _timer) / timeToSpawn;
        }
    }
    public float FrameDeltaTime
    {
        set
        {
            if (Timer >= timeToSpawn)
                return;

            Timer += value;
            if (Timer >= timeToSpawn)
                Activate();
        }
    }
    public float timeToSpawn;

    public Slider refreshSlider;
    public Image coverImage;

    public bool IsAvaliable()
    {
        return Timer >= timeToSpawn;
    }

    public void Activate()
    {
        refreshSlider.enabled = false;
        refreshSlider.value = 0;

        coverImage.enabled = false;
        Timer = timeToSpawn;
    }

    public void Deactivate()
    {
        refreshSlider.enabled = true;
        refreshSlider.value = 1;

        coverImage.enabled = true;
        Timer = 0f;
    }
}
