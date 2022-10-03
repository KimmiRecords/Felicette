using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : IPowerUp
{
    void Activate(Ship ship)
    {
        ship.moveSpeed = ship.moveSpeed + 2f;
    }
}
