using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyPowerUp : IPowerUp
{
    Ship _ship;

    public EmptyPowerUp(Ship ship)
    {
        _ship = ship;
    }

    public void Activate()
    {
        Debug.Log("nada");
    }
}
