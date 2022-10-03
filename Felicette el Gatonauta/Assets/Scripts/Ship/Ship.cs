using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BasePositionDirection
{
    left,
    right
}

public class Ship : MonoBehaviour
{
    //la clase base de la nave. despues puede servir para otros tipos de naves

    public Rigidbody myRigidBody;
    public float thrusterPower;
    public float moveSpeed;
    public float basePositionMoveSpeed;
    public float maxGas;
    public float burnFactor;
    public float _bonusGas;
    public float _bonusSpeed;
    public float boostTime;

    float _currentGas;

    public float CurrentGas
    {
        get
        {
            return _currentGas;
        }

        set
        {
            _currentGas = value;

            if (_currentGas < 0)
            {
                _currentGas = 0;
            }

            if (_currentGas > maxGas)
            {
                _currentGas = maxGas;
            }
        }
    }

    IPowerUp[] _powers = new IPowerUp[4];
    IPowerUp _currentPowerUp;

    void Awake()
    {
        _powers[0] = new EmptyPowerUp(this);
        _powers[1] = new GasPowerUp(this);
        _powers[2] = new SpeedPowerUp(this);
        _powers[3] = new CoinPowerUp(this);
        //_powers[4] = new ShieldPowerUp();

        EventManager.Subscribe(Evento.CajaPickup, SetCurrentPowerUp);
        EventManager.Subscribe(Evento.PowerUpButtonUp, ActivatePowerUp);


        _currentPowerUp = _powers[0];
        print("ship awake");
    }

    void Start()
    {
        
        print("ship start");

    }

    public void SetCurrentPowerUp(params object[] parameters)
    {
        print("setcurrentpowerup");

        if (parameters[0] is int)
        {
            switch ((int)parameters[0])
            {
                case 0: //si en la caja sale 0, es gas powerup
                    _currentPowerUp = _powers[1];
                    break;
                case 1: //si sale 1, es speed powerup
                    _currentPowerUp = _powers[2];
                    break;
                case 2:
                    _currentPowerUp = _powers[3];
                    break;
            }
        }
        else
        {
            print("ojo, no me pasaste int de primer parametro");
        }
    }

    public void ActivatePowerUp(params object[] parameters)
    {
        //lo activo y lo hago empty de nuevo
        print("activate powerup");
        if (_currentPowerUp == _powers[0])
        {
            AudioManager.instance.PlayByNamePitch("PickupReversedSFX", 0.8f);
        }
        else
        {
            AudioManager.instance.PlayByName("PickupSFX");
        }

        _currentPowerUp.Activate();
        _currentPowerUp = _powers[0];
    }

    public void StartBoost()
    {
        StartCoroutine(Boost());
    }

    public IEnumerator Boost()
    {
        //pasan cosas
        print("arranca el boost");
        myRigidBody.mass *= 0.5f;

        yield return new WaitForSeconds(boostTime);

        //terminan de pasar cosas
        print("termina el boost");
        myRigidBody.mass *= 2f;
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

