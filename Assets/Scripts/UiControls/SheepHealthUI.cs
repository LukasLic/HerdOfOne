using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SheepHealthUI : MonoBehaviour
{
    public float maxFoodAlpha;

    //public Text VictoryCanvasText;
    public float okHunger;
    public float okHealth;

    public RawImage hpDamage;
    private Color hpColor;
    public float hpFlashTime;
    private float hpTimer;

    public RawImage criticalFood;
    private Color foodColor;
    private bool increasing;
    private bool foodFlashing;
    public float foodFlashTime;
    private float foodtimer;

    public SheepHealth health;

    private bool gameRunning;
    private bool hpFlashing;

    private void Start()
    {
        foodFlashing = false;
        gameRunning = true;
        hpTimer = 0f;
        foodtimer = 0f;
        hpFlashing = false;
        hpColor = new Color(hpDamage.color.r, hpDamage.color.g, hpDamage.color.b, 0f);
        foodColor = new Color(criticalFood.color.r, criticalFood.color.g, criticalFood.color.b, 0f);
    }

    private void Update()
    {
        if (!gameRunning)
            return;

        if(hpFlashing)
        {
            hpTimer += Time.deltaTime;
            var alpha = (hpFlashTime - hpTimer) / hpFlashTime;

            hpColor.a = alpha;
            hpDamage.color = hpColor;

            if(hpTimer >= hpFlashTime)
            {
                hpFlashing = false;
                hpColor.a = 0f;
                hpDamage.color = hpColor;
            }                
        }


        //Debug.LogWarning(health.Fullnes + " <= " + okHunger);
        if(health.Fullnes <= okHunger && !foodFlashing)
        {
            foodFlashing = true;
            increasing = true;
            foodColor.a = 0f;
            criticalFood.color = foodColor;
        }
        else if(health.Fullnes >= okHunger && foodFlashing)
        {
            foodFlashing = false;
            foodColor.a = 0f;
            criticalFood.color = foodColor;
        }

        // Flash the food overlay.
        if(foodFlashing)
        {
            //Debug.LogWarning("FLLAAASS");
            if(increasing)
            {
                foodtimer += Time.deltaTime;
                var alpha = foodtimer / hpFlashTime;

                alpha = Mathf.Lerp(0f, maxFoodAlpha, alpha);
                foodColor.a = alpha;
                criticalFood.color = foodColor;

                if (foodtimer >= foodFlashTime)
                {
                    foodtimer = 0f;
                    increasing = false;

                    foodColor.a = maxFoodAlpha;
                    criticalFood.color = foodColor;
                }
            }
            else
            {
                foodtimer += Time.deltaTime;
                var alpha = (hpFlashTime - foodtimer) / hpFlashTime;

                alpha = Mathf.Lerp(0f, maxFoodAlpha, alpha);
                foodColor.a = alpha;
                criticalFood.color = foodColor;

                if (foodtimer >= foodFlashTime)
                {
                    foodtimer = 0f;
                    increasing = true;

                    foodColor.a = 0f;
                    criticalFood.color = foodColor;
                }
            }
        }
    }

    public void TakeDamage()
    {
        hpFlashing = true;
        hpTimer = 0f;
    }

    public void Victory()
    {
        gameRunning = false;

        hpColor.a = 0f;
        hpDamage.color = hpColor;

        foodColor.a = 0f;
        criticalFood.color = foodColor;
    }

    public void GameOver()
    {
        gameRunning = false;

        hpColor.a = 0f;
        hpDamage.color = hpColor;

        foodColor.a = 0f;
        criticalFood.color = foodColor;
    }
}
