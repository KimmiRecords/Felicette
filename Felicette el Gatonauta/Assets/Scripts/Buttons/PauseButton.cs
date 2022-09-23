using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseButton : BaseButton
{
    //pone o saca pausa segun lo que le indique en inspector
    //asi uso una sola clase para ambos botones
    public bool isPause;

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (isPause)
        {
            EventManager.Trigger(Evento.PauseButtonUp);
        }
        else
        {
            EventManager.Trigger(Evento.UnpauseButtonUp);
        }
    }
}
