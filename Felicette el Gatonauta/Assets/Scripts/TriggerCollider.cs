using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerCollider : Trigger
{
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
