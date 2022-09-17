using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : TriggerCollider
{
    protected override void OnTriggerEnter(Collider other)
    {
        EventManager.Trigger(Evento.CoinPickup);
        Destroy(this.gameObject);
    }
}
