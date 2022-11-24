using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsTextUpdater : TextUpdater
{
    private void Start()
    {
        UpdateText(LevelManager.instance.Coins);
        EventManager.Subscribe(Evento.CoinUpdate, UpdateText);
    }
}
