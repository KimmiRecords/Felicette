using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PowerUpCooldownSlider : MonoBehaviour
{
    //este script actualiza el slider que representa el tiempo restante de powerup

    public Slider thisSlider;
    public Ship ship;

    void Start()
    {
        thisSlider.maxValue = ship.rescaleDuration;
        thisSlider.value = thisSlider.maxValue;
        //EventManager.Subscribe(Evento.ModoChiquitoStart, StartCooldownTimer);
    }

    public void StartCooldownTimer(params object[] parameters)
    {
        StartCoroutine(CooldownTimer());
    }

    public IEnumerator CooldownTimer()
    {
        print("empieza el timer");
        thisSlider.value = thisSlider.maxValue;
        yield return new WaitForSeconds(1);

        //empieza a vaciar el slider, 1 x segundo
        while (thisSlider.value > 0)
        {
            thisSlider.value -= 1;
            yield return new WaitForSeconds(1);
        }

        print("se termino el timer");
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            //print("destrui a este shipthrusters on isloaded");
        }
        else
        {
            EventManager.Unsubscribe(Evento.ModoChiquitoStart, StartCooldownTimer);

        }
    }
}
