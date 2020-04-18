﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    public Slider healthSlider;
    public Slider foodSlider;
    public Slider timeSlider;

    public void SetHealth(float value, float max)
    {
        healthSlider.value = (value / max);
    }

    public void SetFood(float value, float max)
    {
        foodSlider.value = (value / max);
    }

    public void SetTime(float value, float max)
    {
        timeSlider.value = (value / max);
    }
}