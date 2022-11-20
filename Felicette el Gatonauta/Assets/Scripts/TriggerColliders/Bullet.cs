using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 vectorToTarget;
    float speed;

    public Bullet SetPosition(Vector3 pos)
    {
        transform.position = pos;
        return this;
    }
    public Bullet SetSpeed(float s)
    {
        speed = s;
        return this;
    }

    public Bullet SetTarget(Vector3 tgtPos)
    {
        vectorToTarget = tgtPos - transform.position;
        return this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        TravelToTarget();
    }

    public void TravelToTarget()
    {
        transform.position += (vectorToTarget + (Vector3.up*20)) * speed * Time.deltaTime;
    }


}
