using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeAoE : GravityAoE
{
    public float rechargeFactor;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IGravity>() != null)
        {
            AudioManager.instance.PlayByNamePitch("GravityAoE", 0.7f);
        }
    }

    protected override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
        if (other.GetComponent<IRechargeable>() != null)
        {
            IRechargeable suertudo = other.GetComponent<IRechargeable>();
            suertudo.Recharge(rechargeFactor);
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IGravity>() != null)
        {
            AudioManager.instance.StopByName("GravityAoE");
            if (aoeSpriteRenderer != null)
            {
                aoeSpriteRenderer.color = aoeSpriteOriginalColor;
            }
        }
    }
}
