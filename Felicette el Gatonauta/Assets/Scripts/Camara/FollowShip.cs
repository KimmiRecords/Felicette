using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowShip : MonoBehaviour
{
    //este script se lo adjunto a la camara para que siga a la nave
    //va a seguirlo DESDE la posicion que le asignes en inspector.

    public GameObject targetShip;
    Vector3 offset;


    private void Start()
    {
        offset = transform.position - targetShip.transform.position;
    }
    void Update()
    {

        transform.position = targetShip.transform.position + offset;   
    }
}
