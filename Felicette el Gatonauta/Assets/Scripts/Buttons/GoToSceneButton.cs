using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class GoToSceneButton : BaseButton
{
    //script general para botones que te hacen cmabiar de escena
    public string buttonName;
    public string sceneName;

    string eventName;

    private void Start()
    {
        eventName = buttonName + "ButtonUp";
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        //en el LevelManager dice a donde te lleva cada boton
        EventManager.Trigger(eventName, sceneName);
    }
}
