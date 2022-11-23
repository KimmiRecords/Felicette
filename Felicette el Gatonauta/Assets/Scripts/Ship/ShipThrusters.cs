using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipThrusters : Ship, IGravity
{
    //el "player", partido en los 3 mvc

    public Animator anim;
    public SpriteRenderer itemsSpriteRenderer;
    ShipModel _model;
    ShipView _view;
    ShipController _controller;

    void Start()
    {
        _model = new ShipModel(this);
        _view = new ShipView(_model, anim, itemsSpriteRenderer);
        _controller = new ShipController(_model);

        myRigidBody.useGravity = true;
        myRigidBody = this.gameObject.GetComponent<Rigidbody>();
        gasManager = new ShipGasManager(this);

        EventManager.Subscribe(Evento.AtmosphereWall, EscapeAtmosphere);

    }

    private void FixedUpdate()
    {
        _model.ArtificialUpdate();

        if (!canThrust)
        {
            _model.EndThruster();
            _view.EndThrusterFX();
        }
    }

    public void EscapeAtmosphere(params object[] parameters)
    {
        _model.EscapeAtmosphere();
        _view.EscapeAtmosphereFX();
    }

    public void ApplyGravity(Vector3 planetPosition, float planetMass)
    {
        _model.ApplyGravity(planetPosition, planetMass);
        _view.ApplyGravityFX();
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded) //cuando se destruye porque lo destrui a mano
        {
            //print("destrui a este shipthrusters on isloaded");
        }
        else //cuando se destruye porque cambie de escena
        {
            EventManager.Unsubscribe(Evento.ThrusterDown, _model.StartThruster);
            EventManager.Unsubscribe(Evento.ThrusterDown, _model.ReleaseShip);
            EventManager.Unsubscribe(Evento.ThrusterUp, _model.EndThruster);

            EventManager.Unsubscribe(Evento.AtmosphereWall, EscapeAtmosphere);

            EventManager.Unsubscribe(Evento.ModoChiquitoStart, _view.StartRescale);
            EventManager.Unsubscribe(Evento.CoinRainStart, _view.CoinRainAnimationStart);
            EventManager.Unsubscribe(Evento.ThrusterDown, _view.StartThrusterFX);
            EventManager.Unsubscribe(Evento.ThrusterUp, _view.EndThrusterFX);
            EventManager.Unsubscribe(Evento.EquipItem, _view.EquipItem);
            EventManager.Unsubscribe(Evento.UnequipItem, _view.UnequipItem);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ITriggerCollider>() != null)
        {
            var collider = other.GetComponent<ITriggerCollider>();
            collider.Activate();
        }
    }
}
