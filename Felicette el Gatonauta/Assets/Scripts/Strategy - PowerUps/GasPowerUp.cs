using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasPowerUp : IPowerUp
{
    //este powerup te da extra gas

    public void Activate(Ship s)
    {
        AudioManager.instance.PlayByNamePitch("GasRefill", 1.2f);

        //_ship.CurrentGas = _ship.CurrentGas + _ship.bonusGas;
        EventManager.Trigger(Evento.RefillGas, s.CurrentGas, s.bonusGas); //para que se updatee el slider
        Debug.Log("aumente el gas");

    }
}
