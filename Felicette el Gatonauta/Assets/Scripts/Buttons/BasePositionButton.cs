using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class BasePositionButton : BaseButton
{
    //los baseposition buttons mueven la base antes del despegue
    //despues del despegue, mueven apenitas a la nave

    public BasePositionDirection dir;
    
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        EventManager.Trigger("BasePositionDown", dir);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        EventManager.Trigger("BasePositionUp", dir);
    }

    
}
