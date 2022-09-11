using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThrusterButton : Button
{

    public override void OnPointerDown(PointerEventData eventData)
    {
        EventManager.Trigger("ThrusterDown");
        Debug.Log("pointerdown");
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        EventManager.Trigger("ThrusterUp");
        Debug.Log("pointerup");
    }
}
