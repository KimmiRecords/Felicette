using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipModel
{
    //el model del mvc 

    public Ship ship;
    public Animator anim;
    bool _isThrusting;
    bool _isReleased;

    bool _isMoving;
    BasePositionDirection _dir;
    Vector3 _move;


    public ShipModel(Ship st)
    {
        ship = st;
        
        //EventManager.Subscribe(Evento.AtmosphereWall, EscapeAtmosphere);

        ship.CurrentGas = ship.maxGas;
        _isReleased = false;
    }

    public void ArtificialUpdate()
    {
        if (_isThrusting && ship.canThrust)
        {
            Thruster();
        }

        if (_isMoving && ship.canThrust)
        {
            MoveShip();
        }
    }

    public void ReleaseShip(params object[] parameters)
    {
        //la primera vez que tocas el thrusterbutton, suelta a la nave de su base
        _isReleased = true;
        EventManager.Unsubscribe(Evento.ThrusterDown, ReleaseShip);
    }

    public void StartThruster(params object[] parameters)
    {
        if (ship.canThrust)
        {
            _isThrusting = true;
        }
    }
    public void Thruster()
    {
        //pum para arriba, y consume gas
        ship.myRigidBody.AddForce(ship.transform.up * ship.thrusterPower);
        ship.gasManager.BurnGas();
    }
    public void EndThruster(params object[] parameters)
    {
        _isThrusting = false;
    }

    public void StartMoveShip(params object[] parameters)
    {
        _isMoving = true;

        if (parameters[0] is BasePositionDirection)
        {
            _dir = (BasePositionDirection)parameters[0];
        }
        else
        {
            //print("ojo que el primer parametro que me pasaste no es un BasePositionDirection");
        }
    }
    public void MoveShip()
    {
        if (_dir == BasePositionDirection.left)
        {
            _move = Vector3.left;
        }
        else
        {
            _move = Vector3.right;
        }

        if (_isReleased && ship.canThrust)
        {
            //fuera de la base se mueve mucho pero quema gas
            ship.gasManager.BurnGas();
            ship.transform.position += _move * ship.moveSpeed * Time.deltaTime;
        }
        else
        {
            //en la base no quema gas pero se mueve poquito
            ship.transform.position += _move * ship.basePositionMoveSpeed * Time.deltaTime;
        }
    }
    public void EndMoveShip(params object[] parameters)
    {
        _isMoving = false;
    }

    public void EscapeAtmosphere(params object[] parameters)
    {
        ship.myRigidBody.useGravity = false;
    }

    public void ApplyGravity(Vector3 planetPosition, float planetMass)
    {
        Vector3 grav = GravityForce.GetGravityVector3(ship.myRigidBody.mass, planetMass, Vector3.Distance(ship.transform.position, planetPosition), (planetPosition - ship.transform.position).normalized);
        ship.myRigidBody.AddForce(grav * Time.deltaTime, ForceMode.Force);
    }
}
