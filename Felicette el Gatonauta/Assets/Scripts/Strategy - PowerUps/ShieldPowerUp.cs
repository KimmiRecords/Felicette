using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : IPowerUp
{
    //este tipo de powerup te da un escudo

    public void Activate(Ship s)
    {
        //AudioManager.instance.PlayByName("CoinRain");
        if (!s.isShielded)
        {
            EventManager.Trigger(Evento.GetShield);
            s.isShielded = true;
            Debug.Log("conseguiste escudo");
        }
        else
        {
            Debug.Log("naa no vale, ya tenias escudo");
        }
    }
}
