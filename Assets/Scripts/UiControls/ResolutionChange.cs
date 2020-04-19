using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionChange : MonoBehaviour
{
    private bool usingSmallRes = true;

    public Vector2 smallResolution;
    public Vector2 bigResolution;


    public void SwitchResolution()
    {
        if(usingSmallRes)
        {
            SetRes(bigResolution);
            usingSmallRes = false;
        }
        else
        {
            SetRes(smallResolution);
            usingSmallRes = true;
        }
    }

    private void SetRes(Vector2 newResolution)
    {
        Debug.Log($"Changing resolution to: {(int)newResolution.x}x{(int)newResolution.y}.");
        Screen.SetResolution((int)newResolution.x, (int)newResolution.y, Screen.fullScreen);
    }
}
