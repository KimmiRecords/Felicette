using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerColliderFactory : Factory<TriggerCollider>
{
    //la factory para objetos de tipo triggercollider. 
    //en particular lo usamos para fabricar monedas con el RandomPositionSpawner

    public TriggerColliderFactory(TriggerCollider tc)
    {
        prefab = tc;
    }
}
