using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextTextWall : TriggerCollider
{
    public int nextText;
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        EventManager.Trigger(Evento.NextTextWall, nextText);
    }
}
