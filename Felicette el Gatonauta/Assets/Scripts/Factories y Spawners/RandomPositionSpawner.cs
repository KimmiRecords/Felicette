using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPositionSpawner : MonoBehaviour
{
    //este script fabrica n objetos de tipo triggercollider (como los pickups) en posiciones random

    private TriggerColliderFactory _factory;

    public TriggerCollider prefab;
    public int objectAmount;

    [Header("Random Position Limits")]
    public float minimumX;
    public float maximumX;
    public float minimumY;
    public float maximumY;

    private void Start()
    {
        //le explico al factory cual es el prefab que quiero
        _factory = new TriggerColliderFactory(prefab);

        //los spawneo
        SpawnObjects();
    }

    public void SpawnObjects()
    {
        for (int i = 0; i < objectAmount; i++)
        {
            TriggerCollider instance = _factory.Get();
            instance.transform.position = RandomPosition(minimumX, maximumX, minimumX, maximumY);
            //Debug.Log("spawner: cree el objeto " + instance + " en la posicion " + instance.transform.position);
        }
    }

    public Vector3 RandomPosition(float minX, float maxX, float minY, float maxY)
    {
        Vector3 randomPos;
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);

        randomPos = new Vector3(x, y, 0);
        return randomPos;
    }

}
