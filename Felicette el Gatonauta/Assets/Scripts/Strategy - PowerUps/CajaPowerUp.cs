using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CajaPowerUp : TriggerCollider
{
    //el script de las cajas
    //cuando las chocas, te dan su poder.

    int _thisBoxPowerUp;

    private void Start()
    {
        _thisBoxPowerUp = GetPowerType();
    }

    int GetPowerType()
    {
        int randomPower = Random.Range(0, 3);
        return randomPower;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        //print("rompi la caja, su poder es " + thisBoxPowerUp);
        AudioManager.instance.PlayByName("CrateBreak");
        EventManager.Trigger(Evento.CajaPickup, _thisBoxPowerUp); 
        Destroy(this.gameObject);
    }
}
