using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ShipThrusters : Ship
{
    bool isThrusting;
    bool isRotating;

    Rotation rotationOrientation;

    void Start()
    {
        EventManager.Subscribe("ThrusterDown", StartThruster);
        EventManager.Subscribe("ThrusterUp", EndThruster);

        EventManager.Subscribe("RotateDown", StartRotate);
        EventManager.Subscribe("RotateUp", EndRotate);

        EventManager.Subscribe("AtmosphereWall", EscapeAtmosphere);


        CurrentGas = maxGas;
        myRigidBody.useGravity = true;

    }

    private void FixedUpdate()
    {
        if (isThrusting)
        {
            Thruster();
        }

        if (isRotating)
        {
            Rotate();
        }
    }

    public void StartThruster(params object[] parameters)
    {
        isThrusting = true;
    }

    public void Thruster()
    {
        myRigidBody.AddForce(transform.up * thrusterPower);
        BurnGas();
        //print("thruster: quemando gas....");
    }

    public void EndThruster(params object[] parameters)
    {
        isThrusting = false;
    }




    public void StartRotate(params object[] parameters)
    {
        isRotating = true;
        

        if (parameters[0] is Rotation)
        {
            rotationOrientation = (Rotation)parameters[0];
        }
        else
        {
            print("el primer parametro que me pasaste no es un Rotation");
        }
    }

    public void Rotate()
    {
        float orientationMultiplier = 1;

        switch(rotationOrientation)
        {
            case Rotation.clockwise:
                orientationMultiplier = -1;
                break;

            case Rotation.counterclockwise:
                orientationMultiplier = 1;
                break;
        }

        myRigidBody.AddTorque(transform.forward * rotationPower * orientationMultiplier);
        myRigidBody.AddForce(transform.up * thrusterPower);

        BurnGas();
        //Debug.Log("rotate: girando a la  " + rotationOrientation);
    }

    public void EndRotate(params object[] parameters)
    {
        isRotating = false;
    }

    public void BurnGas()
    {
        CurrentGas -= burnFactor;
        //print("current gas = " + CurrentGas);
        EventManager.Trigger("BurnGas", CurrentGas);
    }


    public void EscapeAtmosphere(params object[] parameters)
    {
        myRigidBody.useGravity = false;
        print("escapaste de la gravedad del planeta");
    }
}
