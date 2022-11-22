using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Orientation
{
    horizontal,
    vertical
}
public class FloatInPlace : MonoBehaviour
{
    //este script se lo adjuntas a un objeto para que oscile en el lugar. 
    //podes elegir amplitud, frecuencia y sentido del movimiento

    [Range(0.1f, 5f)]
    public float amplitude = 1f;
    public float frequency = 1;
    public Orientation orientation = Orientation.vertical;

    Vector3 _dir;
    void Start()
    {
        if (orientation == Orientation.horizontal)
        {
            _dir = Vector3.right;
        }
        else
        {
            _dir = Vector3.up;
        }
    }

    void Update()
    {
        transform.position += _dir * amplitude * Mathf.Sin(frequency * Time.time) * Time.deltaTime;
    }
}
