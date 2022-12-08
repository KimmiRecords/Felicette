using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public enum ItemState
{
    Locked,
    Unlocked
}


public class ShopItemButton : BaseButton
{
    //public string itemName;
    //public int itemCost;
    //public Sprite itemSprite;

    public ShipSkin itemData;

    TMPro.TextMeshProUGUI myTMP;

    Button yo;
    public bool wasPurchased;
    ItemState itemState = ItemState.Locked;

    // Start is called before the first frame update
    void Start()
    {
        yo = GetComponent<Button>();
        myTMP = GetComponentInChildren<TMPro.TextMeshProUGUI>();

        if (!LevelManager.instance.allSkins.ContainsKey(itemData.name))
        {
            ItemButtonsManager.instance.AddButton(itemData.name);
        }

        if (LevelManager.instance.allSkins[itemData.name] == 1)
        {
            //print este item ya estaba comprado
            //print("start shopitembutton - este item ya estaba comprado");
            itemState = ItemState.Unlocked;
            yo.enabled = true;
            yo.interactable = true;
            wasPurchased = true;
            myTMP.text = itemData.name;

        }
        else
        {
            CheckMoney();
        }

        EventManager.Subscribe(Evento.CoinUpdate, CheckMoney);
        //EventManager.Subscribe(Evento.EquipItemButtonUp, UnpressAllButtons);

    }

    public void CheckMoney(params object[] parameters)
    {
        if (LevelManager.instance.allSkins.ContainsKey(itemData.name))
        {
            if (LevelManager.instance.allSkins[itemData.name] == 1)
            {
                return;
            }
        }

        if (LevelManager.instance.Coins >= itemData.cost)
        {
            //print("tenes guita. itemstate unlocked - button enabled");
            itemState = ItemState.Unlocked;
            yo.enabled = true;
            yo.interactable = true;
        }
        else
        {
            //print("no tenes plata campeon. start shopitembutton - button disabled");
            itemState = ItemState.Locked;
            yo.enabled = false;
            yo.interactable = false;
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        switch (itemState)
        {
            case ItemState.Locked:
                //print("no tenes guita para comprar esto");
                break;

            case ItemState.Unlocked:
                if (!wasPurchased)
                {
                    AudioManager.instance.PlayByNamePitch("PickupSFX", 0.8f);
                    EventManager.Subscribe(Evento.ConfirmButtonUp, PopupConfirm);
                    EventManager.Subscribe(Evento.CancelButtonUp, PopupCancel);
                    PopupManager.instance.popupcanvas.SetActive(true);

                    //print("tuki, te compraste y equipaste " + itemName);
                    //AudioManager.instance.PlayByName("PurchaseItem");
                    //AudioManager.instance.PlayByName("EquipItem");
                    //EventManager.Trigger(Evento.EquipItemButtonUp, itemData.sprite, this);
                    //wasPurchased = true;
                    //ItemButtonsManager.instance.Purchase(itemData.name);
                    //myTMP.text = itemData.name;
                    //LevelManager.instance.Coins -= itemData.cost;
                }
                else
                {
                    //print("volviste a equipar " + itemName);
                    AudioManager.instance.PlayByName("EquipItem");
                    EventManager.Trigger(Evento.EquipItemButtonUp, itemData.sprite, this);
                }
                LevelManager.instance.SaveData();
                break;

        }
    }

    public void PopupConfirm(params object[] parameters)
    {
        //print("confirmaste la compra");
        EventManager.Unsubscribe(Evento.ConfirmButtonUp, PopupConfirm);
        EventManager.Unsubscribe(Evento.CancelButtonUp, PopupCancel);
        PopupManager.instance.popupcanvas.SetActive(false);

        AudioManager.instance.PlayByName("PurchaseItem");
        AudioManager.instance.PlayByName("EquipItem");
        EventManager.Trigger(Evento.EquipItemButtonUp, itemData.sprite, this);
        wasPurchased = true;
        ItemButtonsManager.instance.Purchase(itemData.name);
        myTMP.text = itemData.name;
        LevelManager.instance.Coins -= itemData.cost;
    }

    public void PopupCancel(params object[] parameters)
    {
        //print("cancelaste la compra");
        EventManager.Unsubscribe(Evento.ConfirmButtonUp, PopupConfirm);
        EventManager.Unsubscribe(Evento.CancelButtonUp, PopupCancel);
        AudioManager.instance.PlayByNamePitch("PickupReversedSFX", 0.8f);
        PopupManager.instance.popupcanvas.SetActive(false);
    }

    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded)
        {
            EventManager.Unsubscribe(Evento.CoinUpdate, CheckMoney);
        }
    }
}
