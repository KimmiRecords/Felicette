using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResetButton : BaseButton
{
    public override void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("scene reset");
        EventManager.Trigger("ResetButtonUp");
    }

   
}
