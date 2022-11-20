using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSatelite : MonoBehaviour
{
    //este script se lo adjuntas a un planeta y spawnea satelites que orbitan alrededor
    public Satelite satelitePrefab;
    public int sateliteAmount;
    public float orbitRadio;
    public float revolutionSpeed;

    Vector3 firstSatelitePos;

    private void Start()
    {
        firstSatelitePos = transform.position + GetRandomPositionAroundMe(orbitRadio);
        CreateSatelites(sateliteAmount);

    }

    public void CreateSatelites(int amount)
    {
        //creo el primero, luego clono
        Satelite sat = Instantiate(satelitePrefab).
            SetBulletSpeed(1).
            SetColor(Color.blue).
            SetOrbit(true, this.gameObject, revolutionSpeed, orbitRadio).
            SetParent(this.transform).
            SetPosition(firstSatelitePos).
            SetShootingInterval(0.2f).
            SetTotalBullets(3);

        sat.name = "Satelite";


        if (amount > 1)
        {
            for (int i = 0; i < amount-1; i++)
            {
                sat.Clone();
            }
        }
    }

    public Vector3 GetRandomPositionAroundMe(float radio)
    {
        Vector3 pos = Random.onUnitSphere * radio;
        pos.z = 0;

        return pos;
    }

}
