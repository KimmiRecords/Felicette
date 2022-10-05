using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyPowerUp : IPowerUp
{
    //este es el powerup vacio, que tenes por default. 

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
