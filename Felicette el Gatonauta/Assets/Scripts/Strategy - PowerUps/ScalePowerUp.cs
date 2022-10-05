using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePowerUp : IPowerUp
{
    //este powerup te cambia el tamaño de la nave durante unos segundos

    ShipThrusters _ship;

    public ScalePowerUp(ShipThrusters ship)
    {
        _ship = ship;
    }

    public void Activate()
    {
        AudioManager.instance.PlayByName("ModoChiquitoOn");

        Debug.Log("cambio la scale");
        _ship.StartRescale();
    }
}
