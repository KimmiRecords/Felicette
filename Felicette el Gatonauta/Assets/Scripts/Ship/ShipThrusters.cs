using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class ShipThrusters : Ship, IGravity
{
    //la clase principal de nuestra nave
    //controla su rigidbody, y aplica la fuerza que corresponde

    bool isThrusting;
    bool isReleased;
    //bool isRotating;
    //Rotation rotationOrientation;
    
    bool isMoving;
    BasePositionDirection dir;
    Vector3 move;

    void Start()
    {
        EventManager.Subscribe(Evento.ThrusterDown, StartThruster);
        EventManager.Subscribe(Evento.ThrusterDown, ReleaseShip);
        EventManager.Subscribe(Evento.ThrusterUp, EndThruster);

        EventManager.Subscribe(Evento.BasePositionDown, StartMoveShip);
        EventManager.Subscribe(Evento.BasePositionUp, EndMoveShip);

        EventManager.Subscribe(Evento.AtmosphereWall, EscapeAtmosphere);

        CurrentGas = maxGas;
        myRigidBody.useGravity = true;
        isReleased = false;

        myRigidBody = this.gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (isThrusting && CurrentGas > 0)
        {
            Thruster();
        }

        if (isMoving && CurrentGas > 0)
        {
            MoveShip();
        }
    }

    public void ReleaseShip(params object[] parameters)
    {
        //la primera vez que tocas el thrusterbutton, suelta a la nave de su base
        myRigidBody.constraints = RigidbodyConstraints.None;
        myRigidBody.constraints = RigidbodyConstraints.FreezePositionZ;
        isReleased = true;
        EventManager.Unsubscribe(Evento.ThrusterDown, ReleaseShip);
    }

    public void StartThruster(params object[] parameters)
    {
        isThrusting = true;
    }
    public void Thruster()
    {
        //pum para arriba, y consume gas
        myRigidBody.AddForce(transform.up * thrusterPower);
        BurnGas();
        //print("thruster: quemando gas....");
    }
    public void EndThruster(params object[] parameters)
    {
        isThrusting = false;
    }

    public void StartMoveShip(params object[] parameters)
    {
        isMoving = true;

        if (parameters[0] is BasePositionDirection)
        {
            dir = (BasePositionDirection)parameters[0];
        }
        else
        {
            //print("ojo que el primer parametro que me pasaste no es un BasePositionDirection");
        }
    }
    public void MoveShip()
    {
        if (dir == BasePositionDirection.left)
        {
            move = Vector3.left;
        }
        else
        {
            move = Vector3.right;
        }

        if (isReleased)
        {
            BurnGas();
        }

        transform.position += move * moveSpeed * Time.deltaTime;
    }
    public void EndMoveShip(params object[] parameters)
    {
        isMoving = false;
    }

    public void BurnGas()
    {
        CurrentGas -= burnFactor;
        EventManager.Trigger(Evento.BurnGas, CurrentGas);
        //print("current gas = " + CurrentGas);
    }

    public void EscapeAtmosphere(params object[] parameters)
    {
        myRigidBody.useGravity = false;
        //print("escapaste de la gravedad del planeta");
    }

    public void ApplyGravity(Vector3 planetPosition, float planetMass)
    {
        Vector3 grav = GravityForce.GetGravityVector3(myRigidBody.mass, planetMass, Vector3.Distance(transform.position, planetPosition), (planetPosition - transform.position).normalized);

        myRigidBody.AddForce(grav * Time.deltaTime, ForceMode.Force);
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded) //cuando se destruye porque lo destrui a mano
        {
            //print("destrui a este shipthrusters on isloaded");
        }
        else //cuando se destruye porque cambie de escena
        {
            EventManager.Unsubscribe(Evento.ThrusterDown, StartThruster);
            EventManager.Unsubscribe(Evento.ThrusterDown, ReleaseShip);
            EventManager.Unsubscribe(Evento.ThrusterUp, EndThruster);
            EventManager.Unsubscribe(Evento.AtmosphereWall, EscapeAtmosphere);

            //print("destrui a este shipthrusters on sceneclosure");
        }
    }
}
