using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class ShipThrusters : Ship
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
        EventManager.Subscribe("ThrusterDown", StartThruster);
        EventManager.Subscribe("ThrusterDown", ReleaseShip);
        EventManager.Subscribe("ThrusterUp", EndThruster);

        EventManager.Subscribe("BasePositionDown", StartMoveShip);
        EventManager.Subscribe("BasePositionUp", EndMoveShip);

        //EventManager.Subscribe("RotateDown", StartRotate);
        //EventManager.Subscribe("RotateUp", EndRotate);

        EventManager.Subscribe("AtmosphereWall", EscapeAtmosphere);

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

        //if (isRotating)
        //{
        //    Rotate();
        //}
    }

    public void ReleaseShip(params object[] parameters)
    {
        //la primera vez que tocas el thrusterbutton, suelta a la nave de su base
        myRigidBody.constraints = RigidbodyConstraints.None;
        myRigidBody.constraints = RigidbodyConstraints.FreezePositionZ;
        isReleased = true;
        EventManager.Unsubscribe("ThrusterDown", ReleaseShip);
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
            print("ojo que el primer parametro que me pasaste no es un BasePositionDirection");
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
        EventManager.Trigger("BurnGas", CurrentGas);
        //print("current gas = " + CurrentGas);
    }

    public void EscapeAtmosphere(params object[] parameters)
    {
        myRigidBody.useGravity = false;
        print("escapaste de la gravedad del planeta");
    }
    //public void StartRotate(params object[] parameters)
    //{
    //    isRotating = true;
        

    //    if (parameters[0] is Rotation)
    //    {
    //        rotationOrientation = (Rotation)parameters[0];
    //    }
    //    else
    //    {
    //        print("el primer parametro que me pasaste no es un Rotation");
    //    }
    //}

    //public void Rotate()
    //{
    //    float orientationMultiplier = 1;

    //    switch(rotationOrientation)
    //    {
    //        case Rotation.clockwise:
    //            orientationMultiplier = -1;
    //            break;

    //        case Rotation.counterclockwise:
    //            orientationMultiplier = 1;
    //            break;
    //    }

    //    myRigidBody.AddTorque(transform.forward * rotationPower * orientationMultiplier);
    //    //myRigidBody.AddForce(transform.up * thrusterPower);

    //    BurnGas();
    //    //Debug.Log("rotate: girando a la  " + rotationOrientation);
    //}

    //public void EndRotate(params object[] parameters)
    //{
    //    isRotating = false;
    //}
}
