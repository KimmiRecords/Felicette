using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePowerUp : IPowerUp
{
    //este powerup te cambia el tamaño de la nave durante unos segundos

    Ship _ship;

    public ScalePowerUp(Ship ship)
    {
        _ship = ship;
    }

    public void Activate()
    {
        AudioManager.instance.PlayByName("ModoChiquitoOn");

        Debug.Log("cambio la scale");
        EventManager.Trigger(Evento.ModoChiquitoStart);
        //_ship.StartRescale();
    }
}
