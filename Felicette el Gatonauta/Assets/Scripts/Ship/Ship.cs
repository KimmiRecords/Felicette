using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rotation
{
    clockwise,
    counterclockwise
}
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

    float _currentGas;

    //IPowerUp _currentPowerUp;

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
}
