using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerType
{
    Gas = 0,
    Speed = 1,
    Shield = 2
}

public class CajaPowerUp : TriggerCollider
{
    PowerType powerType;

    private void Start()
    {
        powerType = GetPowerType();
    }

    PowerType GetPowerType()
    {
        PowerType power = (PowerType)Random.Range(0, 3);
        return power;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        AudioManager.instance.PlayByNamePitch("PickupSFX", 1.7f);

        print("rompi la caja, su poder es " + powerType);
        EventManager.Trigger(Evento.CajaPickup, powerType);
        Destroy(this.gameObject);
    }
}
