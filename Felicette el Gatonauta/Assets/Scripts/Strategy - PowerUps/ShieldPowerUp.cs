using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : IPowerUp
{
    //este powerup te da un escudo. TODAVIA NO ESTA IMPLEMENTADO.
    Ship _ship;

    public ShieldPowerUp(Ship ship)
    {
        _ship = ship;
    }

    public void Activate()
    {
        //giveshield;
    }
}
