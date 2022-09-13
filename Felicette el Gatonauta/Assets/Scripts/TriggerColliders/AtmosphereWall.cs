using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmosphereWall : TriggerCollider
{
    //cuando atravesas esta pared, se desactiva la gravedad y quedas flotando con la inercia que tenias
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        EventManager.Trigger(Evento.AtmosphereWall);
    }
}
