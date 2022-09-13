using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class SoundToggleButton : BaseButton
{
    Button yo;
    ColorBlock cb;

    Color verde = new Color(0.8f, 1, 0.74f, 1);
    Color rojo = new Color(1, 0.74f, 0.74f, 1);


    private void Start()
    {
        yo = GetComponent<Button>();
        cb = yo.colors;
        print("nacio el soundtogglebutton");
        PaintButton(verde);


        if (AudioManager.instance.SoundOn)
        {
            PaintButton(verde);
        }
        else
        {
            PaintButton(rojo);
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {

    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        AudioManager.instance.SoundOn = !AudioManager.instance.SoundOn;

        if (AudioManager.instance.SoundOn)
        {
            PaintButton(verde);
        }
        else
        {
            PaintButton(rojo);
        }

    }


    public void PaintButton(Color color)
    {
        cb.normalColor = color;
        cb.highlightedColor = color;
        cb.selectedColor = color;
        yo.colors = cb;
        print("cambie el color a " + color);
    }
}
