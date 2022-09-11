using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GasSlider : MonoBehaviour
{
    public Slider gasSlider;
    public Ship ship;

    void Start()
    {
        EventManager.Subscribe("BurnGas", UpdateSlider);
        gasSlider.maxValue = ship.maxGas;
        gasSlider.value = ship.maxGas;
    }

    private void UpdateSlider(params object[] parameters)
    {
        if (parameters[0] is float)
        {
            gasSlider.value = (float)parameters[0];
        }
        //print("gas slider value = " + gasSlider.value);
    }
}
