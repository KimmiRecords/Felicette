using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PowerUpButton : BaseButton
{
    //este es el boton de la ui que cuando lo tocas activas el powerup

    public override void OnPointerUp(PointerEventData eventData)
    {
        //base.OnPointerUp(eventData);
        EventManager.Trigger(Evento.PowerUpButtonUp);
    }
}
