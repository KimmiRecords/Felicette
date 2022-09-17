using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ExitLevelButton : GoToSceneButton
{
    //como GoToSceneButton, pero ademas dispara el evento de exitlevel

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        EventManager.Trigger(Evento.ExitLevelButtonUp);
    }
}
