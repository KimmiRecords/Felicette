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
        EventManager.Subscribe(Evento.RefillGas, OnGasRefill);
    }

    public void BurnGas()
    {
        //Debug.Log(ship.CurrentGas);

        //si no queda mas gas
        if (ship.CurrentGas <= 0)
        {
            //Debug.Log("no hay gas para quemar");
            ship.canThrust = false;
        }

        //tengo gas, quemo
        ship.CurrentGas -= ship.burnFactor;
        EventManager.Trigger(Evento.BurnGas, ship.CurrentGas);
    }

    public void OnGasRefill(params object[] parameters)
    {
        ship.canThrust = true;
        ship.CurrentGas = (float)parameters[0];
    }

    private void OnDestroy()
    {
        if (ship.gameObject.scene.isLoaded)
        {
            //print("destrui a este shipthrusters on isloaded");
        }
        else 
        {
            EventManager.Unsubscribe(Evento.RefillGas, OnGasRefill);
            //print("destrui a este shipthrusters on sceneclosure");
        }
    }


}
