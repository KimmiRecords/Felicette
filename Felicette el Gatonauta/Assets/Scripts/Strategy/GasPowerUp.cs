using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasPowerUp : IPowerUp
{
    void Activate(Ship ship)
    {
        ship.CurrentGas = ship.CurrentGas + 250f;
    }
}
