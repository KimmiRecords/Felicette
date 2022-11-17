using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasPowerUp : IPowerUp
{
    //este powerup te da extra gas

    Ship _ship;

    public GasPowerUp(Ship ship)
    {
        _ship = ship;
    }

    public void Activate()
    {
        AudioManager.instance.PlayByNamePitch("GasRefill", 1.2f);

        _ship.CurrentGas = _ship.CurrentGas + _ship.bonusGas;
        EventManager.Trigger(Evento.RefillGas, _ship.CurrentGas); //para que se updatee el slider
        Debug.Log("aumente el gas");

    }
}
