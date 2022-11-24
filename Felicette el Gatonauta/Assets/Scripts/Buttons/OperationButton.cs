using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class OperationButton : BaseButton
{
    //estos en vez de mandar mensaje, hacen la operacion y ya
    public int coinsToAdd;
    public float staminaToAdd;
    Button yo;

    private void Start()
    {
        yo = GetComponent<Button>();
        EventManager.Subscribe(Evento.CoinUpdate, CheckMoney);
        CheckMoney();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        if (LevelManager.instance.Coins >= Mathf.Abs(coinsToAdd))
        {
            LevelManager.instance.Coins += coinsToAdd;
            LevelManager.instance.Stamina += staminaToAdd;
            //print("pagaste " + coinsToAdd + " para obtener " + staminaToAdd + " stamina");
            AudioManager.instance.PlayByName("StaminaUp");
            LevelManager.instance.SaveData();
        }
    }

    public void CheckMoney(params object[] parameters)
    {
        if (LevelManager.instance.Coins >= Mathf.Abs(coinsToAdd))
        {
            //se puede
            yo.interactable = true;

        }
        else
        {
            //no te alcanza
            yo.interactable = false;
        }
    }

    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded)
        {
            EventManager.Unsubscribe(Evento.CoinUpdate, CheckMoney);
        }
    }
}
