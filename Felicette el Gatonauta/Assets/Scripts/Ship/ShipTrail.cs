using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class ShipTrail : MonoBehaviour
{
    //este script cambia el color del trail de mi nave

    TrailRenderer _myTrailRenderer;
    public Ship myShip;

    void Awake()
    {
        _myTrailRenderer = GetComponent<TrailRenderer>();
    }

    void Start()
    {
        EventManager.Subscribe(Evento.ThrusterDown, StartTrail);
        EventManager.Subscribe(Evento.ThrusterUp, EndTrail);
    }

    public void StartTrail(params object[] parameters)
    {
        if (myShip.CurrentGas > 0)
        {
            _myTrailRenderer.startColor = Color.red;
        }
    }

    void EndTrail(params object[] parameters)
    {
        _myTrailRenderer.startColor = Color.white;
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
        }
    }
}
