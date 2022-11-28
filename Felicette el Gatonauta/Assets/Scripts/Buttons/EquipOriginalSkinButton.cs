using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class EquipOriginalSkinButton : BaseButton
{
    public Sprite itemSprite;


    public override void OnPointerUp(PointerEventData eventData)
    {
        AudioManager.instance.PlayByName("EquipItem");
        EventManager.Trigger(Evento.EquipItemButtonUp, itemSprite, this);
        print("equipaste la nave original");
        
    }
}
