using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : IPowerUp
{
    //este powerup te da extra velocidad durante unos segundos. TODAVIA NO ESTA IMPLEMENTADO.
    ShipThrusters _ship;

    public SpeedPowerUp(ShipThrusters ship)
    {
        _ship = ship;
    }

    public void Activate()
    {
        Debug.Log("aumente la movespeed");
        //_ship.StartBoost();
    }
}
