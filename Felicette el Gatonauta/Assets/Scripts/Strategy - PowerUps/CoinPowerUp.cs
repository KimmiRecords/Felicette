using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPowerUp : IPowerUp
{
    //este tipo de powerup te da una cantidad de monedas random

    Ship _ship;

    public CoinPowerUp(Ship ship)
    {
        _ship = ship;
    }

    public void Activate()
    {
        AudioManager.instance.PlayByName("CoinRain");

        int randomCoins = Random.Range(_ship.randomCoinsMin, _ship.randomCoinsMax);
        for (int i = 0; i < randomCoins; i++)
        {
            LevelManager.instance.AddCoin();
        }
        Debug.Log("ganaste " + randomCoins + " monedas");
    }
}
