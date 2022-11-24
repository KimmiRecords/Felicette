using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : TriggerCollider
{
    public override void Activate()
    {
        base.Activate();
        //AudioManager.instance.PlayByNamePitch("PickupSFX", 2);
        EventManager.Trigger(Evento.CoinPickup);
        Destroy(this.gameObject);
    }

}
