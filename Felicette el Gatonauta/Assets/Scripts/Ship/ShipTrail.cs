using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class ShipTrail : MonoBehaviour
{
    TrailRenderer myTrailRenderer;
    public Ship myShip;

    void Awake()
    {
        
        myTrailRenderer = GetComponent<TrailRenderer>();
        //print("SHIPTRAIL: cargue el trailrenderer: " + myTrailRenderer.name);
    }

    void Start()
    {
        EventManager.Subscribe(Evento.ThrusterDown, StartTrail);
        EventManager.Subscribe(Evento.ThrusterUp, EndTrail);
        //print("SHIPTRAIL: me suscribi a los eventos");
    }

    public void StartTrail(params object[] parameters)
    {
        if (myShip.CurrentGas > 0)
        {
            myTrailRenderer.startColor = Color.red;
        }
        //print("SHIPTRAIL: dispare startTrail");
    }

    void EndTrail(params object[] parameters)
    {
        myTrailRenderer.startColor = Color.white;
        //print("SHIPTRAIL: dispare endTrail");
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded) //cuando se destruye porque lo destrui a mano
        {
            //print("destrui a este shiptrail on isloaded");
        }
        else //cuando se destruye porque cambie de escena
        {
            EventManager.Unsubscribe(Evento.ThrusterDown, StartTrail);
            EventManager.Unsubscribe(Evento.ThrusterUp, EndTrail);
            //print("destrui a este shiptrail on sceneclosure");
        }
    }
}
