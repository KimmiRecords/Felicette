using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    // prende y apaga cosas de la ui dentro de los niveles

    public PowerUpCooldownSlider powerupCooldownSlider;

    void Start()
    {
        powerupCooldownSlider.gameObject.SetActive(false);

        EventManager.Subscribe(Evento.ModoChiquitoStart, TurnOnSlider);
        EventManager.Subscribe(Evento.ModoChiquitoEnd, TurnOffSlider);
    }
    public void TurnOnSlider(params object[] parameters)
    {
        powerupCooldownSlider.gameObject.SetActive(true);
        powerupCooldownSlider.StartCooldownTimer();
    }

    public void TurnOffSlider(params object[] parameters)
    {
        powerupCooldownSlider.gameObject.SetActive(false);
    }

    

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            //print("destrui a este shipthrusters on isloaded");
        }
        else
        {
            EventManager.Unsubscribe(Evento.ModoChiquitoStart, TurnOnSlider);
            EventManager.Unsubscribe(Evento.ModoChiquitoEnd, TurnOffSlider);
        }
    }
}
