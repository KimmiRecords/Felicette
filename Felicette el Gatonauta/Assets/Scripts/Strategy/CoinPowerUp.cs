using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPowerUp : IPowerUp
{
    Ship _ship;

    public CoinPowerUp(Ship ship)
    {
        _ship = ship;
    }

    public void Activate()
    {
        int randomCoins = Random.Range(5, 11);
        for (int i = 0; i < randomCoins; i++)
        {
            LevelManager.instance.AddCoin();
        }
        Debug.Log("ganaste " + randomCoins + " monedas");
    }
}
