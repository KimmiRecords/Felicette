using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class SoundToggleButton : BaseButton
{
    Button _yo;
    ColorBlock _cb;

    //Color verde = new Color(0.8f, 1, 0.74f, 1);
    //Color rojo = new Color(1, 0.74f, 0.74f, 1);
    private void Start()
    {
        _yo = GetComponent<Button>();
        _cb = _yo.colors;
        PaintButton(Color.green);

        if (AudioManager.instance.SoundOn)
        {
            PaintButton(Color.green);
        }
        else
        {
            PaintButton(Color.red);
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        AudioManager.instance.SoundOn = !AudioManager.instance.SoundOn;

        if (AudioManager.instance.SoundOn)
        {
            PaintButton(Color.green);
        }
        else
        {
            PaintButton(Color.red);
        }
    }

    public void PaintButton(Color color)
    {
        _cb.normalColor = color;
        _cb.highlightedColor = color;
        _cb.selectedColor = color;
        _yo.colors = _cb;
    }
}
