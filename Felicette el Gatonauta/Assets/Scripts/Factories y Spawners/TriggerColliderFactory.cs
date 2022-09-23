using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerColliderFactory : Factory<TriggerCollider>
{
    public TriggerColliderFactory(TriggerCollider tc)
    {
        prefab = tc;
    }
}
