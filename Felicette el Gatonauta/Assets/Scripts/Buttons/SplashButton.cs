using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SplashButton : GoToSceneButton
{
    public override void OnPointerUp(PointerEventData eventData)
    {
        EventManager.Trigger(Evento.SplashButtonUp);
        //LevelManager.instance.SaveData();
        base.OnPointerUp(eventData);
    }
}
