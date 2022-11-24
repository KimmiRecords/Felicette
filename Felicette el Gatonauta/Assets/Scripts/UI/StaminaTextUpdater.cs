using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaTextUpdater : TextUpdater
{
    void Start()
    {
        UpdateText(LevelManager.instance.Stamina);
        EventManager.Subscribe(Evento.StaminaUpdate, UpdateText);
    }

    public override void UpdateText(params object[] parameters)
    {
        if (parameters[0] is float)
        {
            myTMP.text = baseText + (float)parameters[0] + "/" + LevelManager.instance.maxStamina;
        }
    }
}
