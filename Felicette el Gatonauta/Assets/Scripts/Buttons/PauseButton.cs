using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseButton : BaseButton
{
    public bool isPause;
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (isPause)
        {
            EventManager.Trigger(Evento.PauseButtonUp);
            print("pausebutton: isPause es true, triggeree el pausebuttonup");
        }
        else
        {
            EventManager.Trigger(Evento.UnpauseButtonUp);
            print("pausebutton: isPause es false, triggeree el unpausebuttonup");

        }
    }
}
