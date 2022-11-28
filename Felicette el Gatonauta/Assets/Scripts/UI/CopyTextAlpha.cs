using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CopyTextAlpha : MonoBehaviour
{
    public Image yo;
    public TMPro.TextMeshProUGUI aQuienCopio;
    
    
    void Update()
    {
        yo.color = new Color(yo.color.r, yo.color.g, yo.color.b, Mathf.Clamp(aQuienCopio.color.a, 0, 0.3f));
    }
}
