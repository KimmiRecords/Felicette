using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EraseDataButton : GoToSceneButton
{
    // borra todo gg
    public override void OnPointerUp(PointerEventData eventData)
    {
        
        AudioManager.instance.PlayByNamePitch("PickupReversedSFX", 0.5f);
        EventManager.Trigger(Evento.EraseDataButtonUp);

        EventManager.Trigger(Evento.GoToSceneButtonUp, "MainMenu");


    }
}
