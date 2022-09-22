using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TutorialFrame : MonoBehaviour
{
    [Range(-1,0.5f)]
    public float amplitude;
    public float frequency;
    public float offset;

    Image yo;

    Color targetColor;



    void Start()
    {
        yo = GetComponent<Image>();
        targetColor = yo.color;
    }

    void Update()
    {
        targetColor.a = amplitude * Mathf.Sin(frequency * Time.time) + offset;
        targetColor.a = Mathf.Clamp(targetColor.a, 0, 1);
        print(targetColor.a);

        yo.color = targetColor;
    }
}
