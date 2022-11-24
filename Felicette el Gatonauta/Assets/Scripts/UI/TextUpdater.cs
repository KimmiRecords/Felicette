using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TextUpdater : MonoBehaviour
{
    public TMPro.TextMeshProUGUI myTMP;
    public string baseText;

    public virtual void UpdateText(params object[] parameters)
    {
        if (parameters[0] is int)
        {
            myTMP.text = baseText + (int)parameters[0];
        }
    }
}
