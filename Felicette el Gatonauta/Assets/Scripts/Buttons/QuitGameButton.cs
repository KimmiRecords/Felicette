using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class QuitGameButton : BaseButton
{
    public override void OnPointerUp(PointerEventData eventData)
    {
        AudioManager.instance.PlayByNamePitch("PickupSFX", 0.8f);
        EventManager.Subscribe(Evento.ConfirmButtonUp, PopupConfirm);
        EventManager.Subscribe(Evento.CancelButtonUp, PopupCancel);
        PopupManager.instance.popupcanvas.SetActive(true);
    }

    public void PopupConfirm(params object[] parameters)
    {
        //print("confirmaste salir");
        EventManager.Unsubscribe(Evento.ConfirmButtonUp, PopupConfirm);
        EventManager.Unsubscribe(Evento.CancelButtonUp, PopupCancel);
        PopupManager.instance.popupcanvas.SetActive(false);

        EventManager.Trigger(Evento.QuitGameButtonUp);
    }

    public void PopupCancel(params object[] parameters)
    {
        //print("cancelaste salir");
        EventManager.Unsubscribe(Evento.ConfirmButtonUp, PopupConfirm);
        EventManager.Unsubscribe(Evento.CancelButtonUp, PopupCancel);
        AudioManager.instance.PlayByNamePitch("PickupReversedSFX", 0.8f);
        PopupManager.instance.popupcanvas.SetActive(false);
    }
}
