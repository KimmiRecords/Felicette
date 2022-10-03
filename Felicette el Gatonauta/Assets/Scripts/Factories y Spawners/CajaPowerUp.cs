using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CajaPowerUp : TriggerCollider
{
    //el script de las cajas
    //cuando las chocas, te dan su poder.

    int thisBoxPowerUp;

    private void Start()
    {
        thisBoxPowerUp = GetPowerType();
    }

    int GetPowerType()
    {
        int randomPower = Random.Range(0, 3);
        return randomPower;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        AudioManager.instance.PlayByNamePitch("PickupSFX", 1.7f);

        print("rompi la caja, su poder es " + thisBoxPowerUp);
        EventManager.Trigger(Evento.CajaPickup, thisBoxPowerUp); 
        Destroy(this.gameObject);
    }
}
