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
    [Range(0.01f, 0.1f)]
    public float amplitude = 0.05f;
    public float frequency = 1;
    public Orientation orientation = Orientation.vertical;

    Vector3 dir;
    void Start()
    {
        if (orientation == Orientation.horizontal)
        {
            dir = Vector3.right;
        }
        else
        {
            dir = Vector3.up;
        }
    }

    void Update()
    {
        transform.position += dir * amplitude * Mathf.Sin(frequency * Time.time);
    }
}
