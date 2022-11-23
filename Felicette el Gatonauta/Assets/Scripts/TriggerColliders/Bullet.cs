using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 vectorToTarget;
    float speed;
    float predictionAmount;

    float bulletLifetime = 2;
    float timer;

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

    private void Start()
    {
        predictionAmount = Random.Range(5f, 20f);
        timer = 0;

    }
    void Update()
    {
        TravelToTarget();
        timer += Time.deltaTime;

        if (timer > bulletLifetime)
        {
            print("la bala supero su vida util");
            MeDevuelvo();
        }
    }

    public void TravelToTarget()
    {
        transform.position += (vectorToTarget + (Vector3.up * predictionAmount)) * speed * Time.deltaTime;
    }

    public static void TurnOn(Bullet b)
    {
        print("BULLET: prendo la bala");
        //b.transform.position = BulletSpawner.RandomPosition();
        b.gameObject.SetActive(true);
    }

    public static void TurnOff(Bullet b)
    {
        print("BULLET: apago la bala");
        b.gameObject.SetActive(false);
    }


    private void OnBecameInvisible()
    {
        print("BULLET: me hice invi");
        MeDevuelvo();
    }

    public void MeDevuelvo()
    {
        print("BULLET: me devuelvo");
        SateliteManager.instance.ReturnBullet(this);
    }

}
