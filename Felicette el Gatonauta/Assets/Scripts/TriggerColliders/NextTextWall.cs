using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextTextWall : TriggerCollider
{
    //un tipo de wall que avisa al tutorial text manager cual texto disparar
    public int nextText;

    //protected override void OnTriggerEnter(Collider other)
    //{
    //    base.OnTriggerEnter(other);
    //    EventManager.Trigger(Evento.NextTextWall, nextText);
    //}

    public override void Activate()
    {
        base.Activate();
        EventManager.Trigger(Evento.NextTextWall, nextText);
    }
}
