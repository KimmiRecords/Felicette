using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TextUpdater : MonoBehaviour
{
    public TMPro.TextMeshProUGUI myTMP;

    public abstract void UpdateText(params object[] parameters);
}
