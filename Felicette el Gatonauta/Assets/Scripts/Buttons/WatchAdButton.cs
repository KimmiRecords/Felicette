using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class WatchAdButton : BaseButton
{
    //los baseposition buttons mueven la base antes del despegue
    //despues del despegue, mueven la nave hacia los lados

    [SerializeField] string adType = "Rewarded_Android";
    [SerializeField] RewardType adRewardType = RewardType.coins;


    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        EventManager.Trigger(Evento.WatchAdButtonUp, adType, adRewardType);
    }



}
