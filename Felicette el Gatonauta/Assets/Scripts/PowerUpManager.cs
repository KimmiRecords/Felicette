using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{

    IPowerUp[] _powers = new IPowerUp[4];
    IPowerUp _currentPowerUp;

    public Ship ship;


    //las sagradas escrituras:
    public string[] allPowerUpTexts = new string[4];
    public Sprite[] allPowerUpSprites = new Sprite[4];


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
        //powerup manager la tiene clara y sabe cual powerup es el 1, el 2, el 3, etc.
        //entonces aviso a cada uno que necesita enterarse de esto, en el idioma que necesita

        //print("setcurrentpowerup");
        if (parameters[0] is int)
        {
            _currentPowerUp = _powers[(int)parameters[0]];
        }
        else
        {
            print("ojo, no me pasaste int de primer parametro");
        }

        EventManager.Trigger(Evento.GotPowerUp, 
                                                parameters[0], 
                                                allPowerUpTexts[(int)parameters[0]], 
                                                allPowerUpSprites[(int)parameters[0]]);
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
        if (!gameObject.scene.isLoaded) //cuando se destruye porque lo destrui a mano
        {
            EventManager.Unsubscribe(Evento.CajaPickup, SetCurrentPowerUp);
            EventManager.Unsubscribe(Evento.PowerUpButtonUp, ActivatePowerUp);
            //print("destrui a este shipthrusters on isloaded");
        }
        
    }
}
