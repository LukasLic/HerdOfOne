using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliciousBush : Bush
{
    public override void OnSpawn()
    {
        base.OnSpawn();

        // Change focus of the sheep.
        var sheep = GameObject.FindGameObjectWithTag("Sheep").GetComponent<SheepBehaviour>();
        sheep.DeliciousBushAppeared();
    }
}
