using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : IPowerUp
{
    Ship _ship;
    float boostTime;

    public SpeedPowerUp(Ship ship)
    {
        _ship = ship;
    }

    public void Activate()
    {
        Debug.Log("aumente la movespeed");
        _ship.StartBoost();
    }
}
