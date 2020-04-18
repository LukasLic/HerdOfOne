using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliciousBush : Bush
{
    public override void OnSpawn()
    {
        Debug.Log("Change FOCUS");
        var sheep = GameObject.FindGameObjectWithTag("Sheep").GetComponent<SheepBehaviour>();
        sheep.ForgetBush();
    }
}
