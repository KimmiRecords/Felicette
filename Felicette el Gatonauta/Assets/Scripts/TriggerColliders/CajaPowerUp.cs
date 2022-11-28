using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CajaPowerUp : TriggerCollider
{
    //el script de las cajas
    //en el start, se genera random qu� tipo de poder da. luego el powerup manager se encarga del resto

    int _thisBoxPowerUp;

    private void Start()
    {
        _thisBoxPowerUp = GetPowerType();
    }

    int GetPowerType()
    {
        //produce un valor random de 1 a 3, y avisa al powerupmanager, que sabe cual es cual
        int randomPower = Random.Range(1, 4);
        return randomPower;
    }

    public override void Activate()
    {
        base.Activate();
        AudioManager.instance.PlayByName("CrateBreak");
        EventManager.Trigger(Evento.CajaPickup, _thisBoxPowerUp);
        Destroy(this.gameObject);
    }
}
