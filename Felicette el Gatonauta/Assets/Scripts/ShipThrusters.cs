using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipThrusters : Ship
{
    bool isThrusting;
    void Start()
    {
        EventManager.Subscribe("ThrusterDown", StartThruster);
        EventManager.Subscribe("ThrusterUp", EndThruster);
    }

    private void FixedUpdate()
    {
        if (isThrusting)
        {
            Thruster();
        }
    }

    public void StartThruster(params object[] parameters)
    {
        isThrusting = true;
    }

    public void Thruster()
    {
        myRigidBody.AddForce(transform.up * thrusterPower);
        print("thruster: quemando gas....");
    }

    public void EndThruster(params object[] parameters)
    {
        isThrusting = false;
    }
}
