using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipThrusters : Ship, IGravity, IRechargeable
{
    //el "player", partido en los 3 mvc
    public Animator anim;
    public SpriteRenderer naveSpriteRenderer;
    public GameObject fuegoGameObject;
    public GameObject shieldGameObject;
    ShipModel _model;
    ShipView _view;
    ShipController _controller;

    bool shieldWasBrokenAlready = false;

    void Start()
    {
        _model = new ShipModel(this);
        _view = new ShipView(this);
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

    public void Recharge(float rechargeFactor)
    {
        gasManager.Recharge(rechargeFactor);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Obstacle>() != null)
        {
            //print("es un obstaculo");
            var obstaculo = other.GetComponent<Obstacle>();
            if (isShielded)
            {
                //print("perdiste el escudo");
                if (!shieldWasBrokenAlready)
                {
                    Invoke("LoseShield", 0.75f);
                }
                AudioManager.instance.PlayByNamePitch("Hurt", 0.5f);
                //start shield breaking animation
                shieldWasBrokenAlready = true;
            }
            else
            {
                obstaculo.Activate();
            }
        }
        else if (other.GetComponent<ITriggerCollider>() != null)
        {
            //print("es solo un triggercollider");
            var collider = other.GetComponent<ITriggerCollider>();
            collider.Activate();
        }
    }
    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded)
        {
            //print("destrui a este shipthrusters on isloaded");
            EventManager.Unsubscribe(Evento.ThrusterDown, _model.StartThruster);
            EventManager.Unsubscribe(Evento.ThrusterDown, _model.ReleaseShip);
            EventManager.Unsubscribe(Evento.ThrusterUp, _model.EndThruster);
            EventManager.Unsubscribe(Evento.AtmosphereWall, EscapeAtmosphere);
            EventManager.Unsubscribe(Evento.ModoChiquitoStart, _view.StartRescale);
            EventManager.Unsubscribe(Evento.CoinRainStart, _view.CoinRainAnimationStart);
            EventManager.Unsubscribe(Evento.ThrusterDown, _view.StartThrusterFX);
            EventManager.Unsubscribe(Evento.ThrusterUp, _view.EndThrusterFX);
            EventManager.Unsubscribe(Evento.StartDeathSequence, _view.StartDeathSequence);
            EventManager.Unsubscribe(Evento.GetShield, _view.GetShieldFX);

        }
    }

    public void LoseShield()
    {
        isShielded = false;
        _view.LoseShieldFX();
    }
}
