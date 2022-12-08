using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CajaPowerUp : TriggerCollider
{
    //el script de las cajas
    //en el start, se genera random qué tipo de poder da. luego el powerup manager se encarga del resto

    int _thisBoxPowerUp;

    [Header("Si el powerup esta Seteado o es Random")]
    public bool isSet = false;
    //public int powerUpIndex = 0;
    public PowerUpTypes type;

    private void Start()
    {
        _thisBoxPowerUp = GetPowerType();
    }

    int GetPowerType()
    {
        //produce un valor random de 1 a 3, y avisa al powerupmanager, que sabe cual es cual
        int power;
        if (isSet)
        {
            power = (int)type;
        }
        else
        {
            power = Random.Range(1, (int)PowerUpTypes.Count);
        }
        return power;
    }

    public override void Activate()
    {
        base.Activate();
        AudioManager.instance.PlayByName("CrateBreak");
        EventManager.Trigger(Evento.CajaPickup, _thisBoxPowerUp);
        Destroy(this.gameObject);
    }
}
