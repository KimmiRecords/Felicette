using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateButton : Button
{
    public Rotation rotateOrientation;

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        EventManager.Trigger("RotateDown", rotateOrientation);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        EventManager.Trigger("RotateUp", rotateOrientation);
    }
}
