using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSatelite : MonoBehaviour
{
    //este script se lo adjuntas a un planeta y spawnea satelites que orbitan alrededor

    [Header("Satelite Settings")]
    public Satelite satelitePrefab;
    public int sateliteAmount;
    public float orbitRadio;
    public float revolutionSpeed;

    [Header("Bullet Settings")]
    public float bulletSpeed;
    public float shootingInterval;
    public int totalBullets;

    private void Start()
    {
        if (sateliteAmount > 0)
        {
            CreateSatelites(sateliteAmount);
        }
    }

    public void CreateSatelites(int amount)
    {
        //creo el primero, luego clono
        Satelite sat = Instantiate(satelitePrefab).
            SetBulletSpeed(bulletSpeed).
            SetColor(Color.blue).
            SetOrbit(true, this.gameObject, revolutionSpeed, orbitRadio).
            SetParent(this.transform).
            SetRandomPositionInCircleAroundTarget(this.transform, orbitRadio).
            SetShootingInterval(shootingInterval).
            SetTotalBullets(totalBullets);

        sat.name = "Satelite";

        if (amount > 1)
        {
            for (int i = 0; i < amount-1; i++)
            {
                sat.Clone();
            }
        }
    }
}
