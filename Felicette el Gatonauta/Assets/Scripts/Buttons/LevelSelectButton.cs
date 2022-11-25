using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class LevelSelectButton : GoToSceneButton
{
    //entrar a un nivel cuesta N Stamina de entrada.
    public int staminaCost;
    bool _canPayEntrance = false;
    Button yo;

    void Start()
    {
        yo = GetComponent<Button>();
        yo.interactable = false;
        EventManager.Subscribe(Evento.StaminaUpdate, CheckStamina);
        CheckStamina();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        CheckStamina();

        if (_canPayEntrance)
        {
            //print("tenias guita, pagaste el stamina y entras al nivel");
            AudioManager.instance.PlayByNamePitch("StaminaUp", 0.5f);
            LevelManager.instance.myStaminaSystem.UseEnergy(staminaCost); //pago el stamina
            LevelManager.instance.SaveData(); //pago el stamina

            base.OnPointerUp(eventData); //ir a la escena
        }
    }


    public void CheckStamina(params object[] parameters)
    {
        if (staminaCost > 0)
        {
            if (LevelManager.instance.Stamina >= staminaCost)
            {
                _canPayEntrance = true;
                yo.interactable = true;
            }
            else
            {
                _canPayEntrance = false;
                yo.interactable = false;
            }
        }
        else
        {
            _canPayEntrance = true;
            yo.interactable = true;
        }
    }

    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded)
        {
            EventManager.Unsubscribe(Evento.StaminaUpdate, CheckStamina);
        }
    }
}
