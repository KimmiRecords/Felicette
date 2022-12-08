using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BasePositionDirection
{
    left,
    right
}

public abstract class Ship : MonoBehaviour
{
    //la clase base de la nave. despues puede servir para otros tipos de naves

    public Rigidbody myRigidBody;
    public float thrusterPower;
    public float moveSpeed;
    public float basePositionMoveSpeed;
    public float maxGas;
    public float burnFactor;

    [Header("Powerup Settings")]
    public float bonusGas;
    public float rescaleFactor;
    public float rescaleDuration;
    public int randomCoinsMin;
    public int randomCoinsMax;

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

    public bool canThrust;
    public bool isShielded;

    public ShipGasManager gasManager;
}

