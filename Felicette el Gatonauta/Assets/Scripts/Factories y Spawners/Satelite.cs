using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Satelite : Prototype, ITriggerCollider
{
    float bulletSpeed;
    float shootingInterval;
    int totalBullets;

    bool orbits = false;
    GameObject revolutionTarget;
    float revolutionSpeed;
    float orbitRadio;

    public Satelite SetColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
        return this;
    }

    public Satelite SetPosition(Vector3 pos)
    {
        transform.position = pos;
        return this;
    }

    public Satelite SetPosition(float x = 0, float y = 0, float z = 0)
    {
        transform.position = new Vector3(x, y, z);
        return this;
    }

    public Satelite SetBulletSpeed(float s)
    {
        bulletSpeed = s;
        return this;
    }

    public Satelite SetShootingInterval(float s)
    {
        shootingInterval = s;
        return this;
    }

    public Satelite SetTotalBullets(int b)
    {
        totalBullets = b;
        return this;
    }

    public Satelite SetOrbit(bool b, GameObject revTarget, float revSpeed, float radiusToTarget)
    {
        orbits = b;
        revolutionTarget = revTarget;
        revolutionSpeed = revSpeed;
        orbitRadio = radiusToTarget;
        return this;
    }

    public Satelite SetParent(Transform t)
    {
        print("set parent = " + t.name);
        transform.parent = t;
        return this;
    }

    public Satelite SetScale(Vector3 s)
    {
        transform.localScale = s;
        return this;
    }



    public override Prototype Clone()
    {
        Satelite sat = Instantiate(this);

        sat.
            SetColor(GetComponent<Renderer>().material.color + Random.ColorHSV(0.1f, 0.2f)).
            SetBulletSpeed(bulletSpeed).
            SetShootingInterval(shootingInterval).
            SetTotalBullets(totalBullets).
            SetOrbit(orbits, revolutionTarget, revolutionSpeed, orbitRadio).
            SetParent(this.transform.parent).
            SetPosition(GetRandomPositionAroundTarget(revolutionTarget, orbitRadio)).
            SetScale(this.transform.localScale);


        return sat;
    }

    private void Update()
    {
        if (orbits)
        {
            transform.RotateAround(revolutionTarget.transform.position, 
                                    revolutionTarget.transform.forward, 
                                    revolutionSpeed * Time.deltaTime);
        }
    }

    public void Activate()
    {
        print("entraste al radio de ataque del satelite");
        StartCoroutine(Shoot());
    }

    public IEnumerator Shoot()
    {
        for (int i = 0; i < totalBullets; i++)
        {
            //instantiate bullet
            print("disparo bala");
            yield return new WaitForSeconds(shootingInterval);
        }
    }

    public Vector3 GetRandomPositionAroundTarget(GameObject revTarget, float radio)
    {
        //en realidad deberia ser un vector2 random cuya magnitud sea = radio.

        Vector3 pos = revTarget.transform.position + (Random.onUnitSphere * radio);
        pos.z = 0;

        return pos;
    }

}
