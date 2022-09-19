using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : TriggerCollider
{
    protected override void OnTriggerEnter(Collider other)
    {
        AudioManager.instance.PlayByNamePitch("PickupSFX", 2);

        EventManager.Trigger(Evento.CoinPickup);
        Destroy(this.gameObject);
    }
}
