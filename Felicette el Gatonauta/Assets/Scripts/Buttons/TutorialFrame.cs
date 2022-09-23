using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TutorialFrame : MonoBehaviour
{
    //el script de los recuadros rojos del tutorial. 
    //la opacidad oscila

    [Range(-1,0.5f)]
    public float amplitude;
    public float frequency;
    public float offset;

    Image _yo;
    Color _targetColor;

    void Start()
    {
        _yo = GetComponent<Image>();
        _targetColor = _yo.color;
    }

    void Update()
    {
        _targetColor.a = amplitude * Mathf.Sin(frequency * Time.time) + offset;
        _targetColor.a = Mathf.Clamp(_targetColor.a, 0, 1);
        _yo.color = _targetColor;
    }
}
