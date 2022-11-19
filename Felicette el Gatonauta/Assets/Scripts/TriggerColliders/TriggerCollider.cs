using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class TriggerCollider : MonoBehaviour, ITriggerCollider
{
    public virtual void Activate()
    {
        //Debug.Log(this.name + ": Activate");
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        //Debug.Log("trigger enter");
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        //Debug.Log("trigger exit");
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        //Debug.Log("trigger stay");
    }
}
