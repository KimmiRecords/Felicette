using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasPowerUp : IPowerUp
{
    Ship _ship;

    public GasPowerUp(Ship ship)
    {
        _ship = ship;
    }

    public void Activate()
    {
        _ship.CurrentGas = _ship.CurrentGas + 250f;
        EventManager.Trigger(Evento.BurnGas, _ship.CurrentGas);
        Debug.Log("aumente el gas");

    }
}
