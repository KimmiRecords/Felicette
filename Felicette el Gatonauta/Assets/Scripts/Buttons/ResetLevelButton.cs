using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ResetLevelButton : BaseButton
{
    //este boton te lleva al ultimo nivel que jugaste
    public override void OnPointerUp(PointerEventData eventData)
    {
        AudioManager.instance.PlayByNamePitch("PickupSFX", 1.1f);
        EventManager.Trigger(Evento.ResetLevelButtonUp);
    }
}
