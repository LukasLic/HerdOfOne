using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    public SheepHealthUI healthUI;

    public Slider healthSlider;
    public Slider foodSlider;
    public Slider timeSlider;

    public Animator VictoryCanvas;
    public Animator GameoverCanvas;

    #region Sliders
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
    #endregion

    public void Victory()
    {
        healthUI.Victory();
        VictoryCanvas.gameObject.SetActive(true);
        VictoryCanvas.SetTrigger("Play");
    }

    public void GameOver()
    {
        healthUI.GameOver();
        GameoverCanvas.gameObject.SetActive(true);
        GameoverCanvas.SetTrigger("Play");
    }
}
