using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class GachaButton : ShopItemButton
{
    //poner el cost en 20
    //poner el sprite y nombre en random

    public ShipSkin[] raros;
    public ShipSkin[] ultrararos;
    public ShipSkin[] legendarios;
    public TMPro.TextMeshProUGUI resultTMP;
    ShipSkin gachaResult;

    public Animator gachaMachineAnimator;

    protected override void Start()
    {
        yo = GetComponent<Button>();
        myTMP = GetComponentInChildren<TMPro.TextMeshProUGUI>();

        for (int i = 0; i < raros.Length; i++)
        {
            if (!LevelManager.instance.allSkins.ContainsKey(raros[i].name))
            {
                ItemButtonsManager.instance.AddButton(raros[i].name);
            }
        }

        for (int i = 0; i < ultrararos.Length; i++)
        {
            if (!LevelManager.instance.allSkins.ContainsKey(ultrararos[i].name))
            {
                ItemButtonsManager.instance.AddButton(ultrararos[i].name);
            }
        }

        for (int i = 0; i < legendarios.Length; i++)
        {
            if (!LevelManager.instance.allSkins.ContainsKey(legendarios[i].name))
            {
                ItemButtonsManager.instance.AddButton(legendarios[i].name);
            }
        }


        CheckMoney();
        EventManager.Subscribe(Evento.CoinUpdate, CheckMoney);
    }

    public override void PopupConfirm(params object[] parameters)
    {
        //print("confirmaste la tirada de gacha");
        EventManager.Unsubscribe(Evento.ConfirmButtonUp, PopupConfirm);
        EventManager.Unsubscribe(Evento.CancelButtonUp, PopupCancel);
        PopupManager.instance.popupcanvas.SetActive(false);

        DetermineGacha();
        GiveGachaResult();
    }

    public void GiveGachaResult()
    {
        //AudioManager.instance.PlayByNamePitch("PurchaseItem", 1.3f);
        AudioManager.instance.PlayByNamePitch("GachaTirar", 1f);

        LevelManager.instance.Coins -= itemData.cost;

        if (gachaResult.name == itemData.name)
        {
            resultTMP.text = "No ganaste nada";
            SetAnimation("Nada");
            //feedback de que no ganaste nada
        }
        else
        {
            //AudioManager.instance.PlayByName("EquipItem");
            //AudioManager.instance.PlayByNamePitch("GachaWin", 1f);
            Invoke("PlayPullSounds", 2);
            EventManager.Trigger(Evento.EquipItemButtonUp, gachaResult.sprite, this);
            wasPurchased = true;
            ItemButtonsManager.instance.Purchase(gachaResult.name);
            resultTMP.text = "Te ganaste un " + gachaResult.name;
            SetAnimation("Pull");
        }

        LevelManager.instance.SaveData();

    }

    public void DetermineGacha()
    {
        float randomRare = Random.Range(0f, 100f);
        int random;
        if (randomRare < 1)
        {
            random = Random.Range(0, legendarios.Length);
            gachaResult = legendarios[random];
            
        }
        else if (randomRare < 5)
        {
            random = Random.Range(0, ultrararos.Length);
            gachaResult = ultrararos[random];
        }
        else if (randomRare < 10)
        {
            random = Random.Range(0, raros.Length);
            gachaResult = raros[random];
        }
        else
        {
            gachaResult = itemData;
        }
    }


    public override void OnPointerUp(PointerEventData eventData)
    {
        //print("aaaaaao");

        switch (itemState)
        {
            case ItemState.Locked:
                print("no tenes guita para comprar esto");
                break;

            case ItemState.Unlocked:
                    AudioManager.instance.PlayByNamePitch("PickupSFX", 0.8f);
                    EventManager.Subscribe(Evento.ConfirmButtonUp, PopupConfirm);
                    EventManager.Subscribe(Evento.CancelButtonUp, PopupCancel);
                    PopupManager.instance.popupcanvas.SetActive(true);
                break;
        }
    }

    public void SetAnimation(string animName)
    {
        gachaMachineAnimator.SetTrigger(animName);
    }

    public void PlayPullSounds()
    {
        AudioManager.instance.PlayByName("EquipItem");
        AudioManager.instance.PlayByNamePitch("GachaWin", 1f);
    }
}
