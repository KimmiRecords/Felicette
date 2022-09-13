using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThrusterButton : BaseButton
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        EventManager.Trigger(Evento.ThrusterDown);

        //print("THRUSTERBUTTON: triggeree el thrusterdown");
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        EventManager.Trigger(Evento.ThrusterUp);

        //print("THRUSTERBUTTON: triggeree el thrusterup");
    }
}
