using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rotation
{
    clockwise,
    counterclockwise
}

public class Ship : MonoBehaviour
{
    public Rigidbody myRigidBody;
    public float thrusterPower;
    public float rotationPower;
    public float maxGas;
    public float burnFactor;

    float _currentGas;

    protected float CurrentGas
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
