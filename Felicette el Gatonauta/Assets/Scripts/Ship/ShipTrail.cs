using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class ShipTrail : MonoBehaviour
{
    TrailRenderer myTrailRenderer;

    void Awake()
    {
        if (myTrailRenderer == null)
        {
            myTrailRenderer = GetComponent<TrailRenderer>();
        }
    }

    void Start()
    {
        EventManager.Subscribe("ThrusterDown", StartTrail);
        EventManager.Subscribe("ThrusterUp", EndTrail);
    }


    void StartTrail(params object[] parameters)
    {
        //myTrailRenderer.startColor = Color.red;
    }

    void EndTrail(params object[] parameters)
    {
        //myTrailRenderer.startColor = Color.white;
    }
}
