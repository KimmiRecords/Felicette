using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController
{
    //el controller del mvc
    //solo se suscribe a los eventos de los botones

    ShipModel _model;

    public ShipController(ShipModel m)
    {
        _model = m;

        EventManager.Subscribe(Evento.ThrusterDown, _model.StartThruster);
        EventManager.Subscribe(Evento.ThrusterDown, _model.ReleaseShip);
        EventManager.Subscribe(Evento.ThrusterUp, _model.EndThruster);
        EventManager.Subscribe(Evento.BasePositionDown, _model.StartMoveShip);
        EventManager.Subscribe(Evento.BasePositionUp, _model.EndMoveShip);
    }

}
