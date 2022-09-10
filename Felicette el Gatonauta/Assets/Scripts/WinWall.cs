using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinWall : TriggerCollider
{
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        EventManager.Trigger("WinWall", "LevelComplete");
    }
}
