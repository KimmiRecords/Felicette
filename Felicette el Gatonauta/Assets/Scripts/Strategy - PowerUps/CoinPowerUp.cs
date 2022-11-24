using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPowerUp : IPowerUp
{
    //este tipo de powerup te da una cantidad de monedas random

    public void Activate(Ship s)
    {
        AudioManager.instance.PlayByName("CoinRain");

        int randomCoins = Random.Range(s.randomCoinsMin, s.randomCoinsMax);
        LevelManager.instance.Coins += randomCoins;

        EventManager.Trigger(Evento.CoinRainStart);
        //Debug.Log("ganaste " + randomCoins + " monedas");
    }
}
