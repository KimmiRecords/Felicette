using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GasSlider : MonoBehaviour
{
    //este script actualiza el slider que muestra la cantidad de gas restante

    public Slider gasSlider;
    public Ship ship;

    void Start()
    {
        EventManager.Subscribe(Evento.BurnGas, UpdateSlider);
        EventManager.Subscribe(Evento.RefillGas, UpdateSlider);
        gasSlider.maxValue = ship.maxGas;
        gasSlider.value = ship.maxGas;
    }

    private void UpdateSlider(params object[] parameters)
    {
        if (parameters[0] is float)
        {
            gasSlider.value = (float)parameters[0];
        }
        else
        {
            print("ojo que no me pasaste un float eh");
        }
    }
}
