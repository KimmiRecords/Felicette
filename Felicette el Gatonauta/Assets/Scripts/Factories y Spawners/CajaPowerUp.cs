using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerType
{
    Gas = 0,
    Speed = 1,
    //Shield = 2
}

public class CajaPowerUp : TriggerCollider
{
    //el script de las cajas
    //cuando las chocas, te dan su poder.
    //falta implementar

    PowerType _powerType;

    private void Start()
    {
        _powerType = GetPowerType();
    }

    PowerType GetPowerType()
    {
        PowerType power = (PowerType)Random.Range(0, 3);
        return power;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        AudioManager.instance.PlayByNamePitch("PickupSFX", 1.7f);

        print("rompi la caja, su poder es " + _powerType);
        EventManager.Trigger(Evento.CajaPickup, _powerType); //la ship deberia hacer algo con esto
        Destroy(this.gameObject);
    }
}
