using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AddCoinButton : BaseButton
{
    public int coinsToAdd;
    public override void OnPointerUp(PointerEventData eventData)
    {
        LevelManager.instance.Coins += coinsToAdd;
        AudioManager.instance.PlayByNamePitch("CoinRain", 2.1f);
    }
}
