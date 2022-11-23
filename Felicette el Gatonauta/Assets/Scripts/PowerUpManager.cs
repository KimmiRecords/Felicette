using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{

    IPowerUp[] _powers = new IPowerUp[4];
    IPowerUp _currentPowerUp;

    public Ship ship;

    void Start()
    {
        _powers[0] = new EmptyPowerUp();
        _powers[1] = new GasPowerUp();
        _powers[2] = new ScalePowerUp();
        _powers[3] = new CoinPowerUp();
        _currentPowerUp = _powers[0];

        EventManager.Subscribe(Evento.CajaPickup, SetCurrentPowerUp);
        EventManager.Subscribe(Evento.PowerUpButtonUp, ActivatePowerUp);
    }

    public void SetCurrentPowerUp(params object[] parameters)
    {
        //print("setcurrentpowerup");
        if (parameters[0] is int)
        {
            _currentPowerUp = _powers[(int)parameters[0]];
        }
        else
        {
            print("ojo, no me pasaste int de primer parametro");
        }

        EventManager.Trigger(Evento.GotPowerUp, parameters[0]);
    }


    public void ActivatePowerUp(params object[] parameters)
    {
        //lo activo y lo hago empty de nuevo
        //print("activate powerup");
        _currentPowerUp.Activate(ship);
        _currentPowerUp = _powers[0];
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded) //cuando se destruye porque lo destrui a mano
        {
            //print("destrui a este shipthrusters on isloaded");
        }
        else //cuando se destruye porque cambie de escena
        {
            EventManager.Unsubscribe(Evento.CajaPickup, SetCurrentPowerUp);
            EventManager.Unsubscribe(Evento.PowerUpButtonUp, ActivatePowerUp);
            //print("destrui a este shipthrusters on sceneclosure");
        }
    }
}
