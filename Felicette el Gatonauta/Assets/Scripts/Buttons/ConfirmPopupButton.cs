using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConfirmPopupButton : BaseButton
{
    //la idea es que sirva para ambos confirmar o cancelar
    public bool isConfirm;

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (isConfirm)
        {
            EventManager.Trigger(Evento.ConfirmButtonUp);
        }
        else
        {
            EventManager.Trigger(Evento.CancelButtonUp);

        }
    }

}
