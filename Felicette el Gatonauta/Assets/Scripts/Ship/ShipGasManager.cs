using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGasManager
{
    //administra el gas. la nave solo pregunta si canThrust

    Ship ship;

    public ShipGasManager(Ship s)
    {
        ship = s;
        ship.canThrust = true;
    }

    public void BurnGas()
    {
        Debug.Log(ship.CurrentGas);

        //si no queda mas gas
        if (ship.CurrentGas <= 0)
        {
            Debug.Log("no hay gas para quemar");
            ship.canThrust = false;
        }

        //tengo gas, quemo
        ship.CurrentGas -= ship.burnFactor;
        EventManager.Trigger(Evento.BurnGas, ship.CurrentGas);
    }

    public void OnGasRefill()
    {
        ship.canThrust = true;
    }



}
