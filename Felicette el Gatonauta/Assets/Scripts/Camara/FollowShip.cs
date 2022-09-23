using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowShip : MonoBehaviour
{
    //este script se lo adjunto a la camara para que siga a la nave

    public Ship ship;
    public Vector3 offset;
 
    void Update()
    {
        transform.position = ship.transform.position + offset;   
    }
}
