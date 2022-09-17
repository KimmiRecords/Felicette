using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CoinsTextUpdater : TextUpdater
{
    public string baseText;

    private void Start()
    {
        UpdateText(LevelManager.instance.Coins);
        EventManager.Subscribe(Evento.CoinUpdate, UpdateText);
    }

    public override void UpdateText(params object[] parameters)
    {
        if (parameters[0] is int)
        {
            myTMP.text = baseText + (int)parameters[0];
        }
    }
}
