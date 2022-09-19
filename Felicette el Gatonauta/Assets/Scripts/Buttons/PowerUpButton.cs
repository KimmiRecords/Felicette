using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PowerUpButton : BaseButton
{
    public override void OnPointerUp(PointerEventData eventData)
    {
        EventManager.Trigger(Evento.PowerUpButtonUp);

        //print("THRUSTERBUTTON: triggeree el thrusterup");
    }
}
