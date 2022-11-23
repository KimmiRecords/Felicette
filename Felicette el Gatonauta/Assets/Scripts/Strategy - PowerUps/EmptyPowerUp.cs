using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyPowerUp : IPowerUp
{
    //este es el powerup vacio, que tenes por default. 

    public void Activate(Ship s)
    {
        Debug.Log("nada");
    }
}
