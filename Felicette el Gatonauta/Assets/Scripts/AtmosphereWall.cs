using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmosphereWall : TriggerCollider
{
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        EventManager.Trigger("AtmosphereWall");
    }
}
