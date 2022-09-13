using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class GoToSceneButton : BaseButton
{
    //script general para botones que te hacen cmabiar de escena
    public string sceneName;

    public override void OnPointerUp(PointerEventData eventData)
    {
        //en el LevelManager dice a donde te lleva cada boton

        if (sceneName == "MainMenu")
        {
            AudioManager.instance.PlayByName("PickupReversedSFX");

        }
        else
        {
            AudioManager.instance.PlayByName("PickupSFX");
        }

        EventManager.Trigger(Evento.GoToSceneButtonUp, sceneName);
    }
}
