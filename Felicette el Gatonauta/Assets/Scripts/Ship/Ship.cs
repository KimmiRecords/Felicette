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

    IPowerUp[] _powers = new IPowerUp[3];
    IPowerUp _currentPowerUp;

    void Awake()
    {
        _powers[0] = new empty();
        _powers[1] = new GasPowerUp(CurrentGas,_bonusGas);
        _powers[2] = new SpeedPowerUp(moveSpeed,_bonusSpeed);
        //_powers[3] = new ShieldPowerUp();
    }

    void Start()
    {
        EventManager.Subscribe(Evento.CajaPickup, SetCurrentPowerUp);
        EventManager.Subscribe(Evento.PowerUpButtonUp, ActivatePowerUp);
    }

    void SetCurrentPowerUp(params object[] parameters)
    {
        _currentPowerUp = parameters[0];

        switch (parameters[0])
        {
            case PowerType.Gas:
                _currentPowerUp = _powers[1];
                break;
            case PowerType.Speed:
                _currentPowerUp = _powers[2];
                break;
        }
    }

    void ActivatePowerUp(params object[] parameters)
    {
        _currentPowerUp.Activate(this);
    }
}
