using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAoE : TriggerCollider
{
    public float planetMass;

    protected override void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<IGravity>() != null)
        {
            var afectado = other.GetComponent<IGravity>();
            afectado.ApplyGravity(transform.position, planetMass);
            //print("gravityAoE: stay");
        }
    }

}
