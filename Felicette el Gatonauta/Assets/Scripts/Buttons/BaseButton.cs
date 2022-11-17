using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("onpointerdown");
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("onpointerup");
    }
}
