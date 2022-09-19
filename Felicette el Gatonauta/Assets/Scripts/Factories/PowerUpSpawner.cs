using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    private CajaFactory _factory;

    public CajaPowerUp cajaPrefab;

    public Vector3[] cajasPosition;

    private void Start()
    {
        _factory = new CajaFactory(cajaPrefab);

        for (int i = 0; i < cajasPosition.Length; i++)
        {
            CreateCaja(cajasPosition[i]);
        }
    }

    void CreateCaja(Vector3 cajaPosition)
    {
        var instance = _factory.Get();
        instance.transform.position = cajaPosition;
    }




}