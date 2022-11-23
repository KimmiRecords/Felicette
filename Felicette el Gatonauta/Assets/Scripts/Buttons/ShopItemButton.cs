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
    public string itemName;
    public int itemCost;
    public Sprite itemSprite;

    Button yo;
    public bool wasPurchased;
    ItemState itemState = ItemState.Locked;

    // Start is called before the first frame update
    void Start()
    {
        yo = GetComponent<Button>();

        if (!LevelManager.instance.allSkins.ContainsKey(itemName))
        {
            ItemButtonsManager.instance.AddButton(itemName);
        }

        if (LevelManager.instance.allSkins[itemName] == 1)
        {
            //print este item ya estaba comprado
            print("start shopitembutton - este item ya estaba comprado");
            itemState = ItemState.Unlocked;
            yo.enabled = true;
            yo.interactable = true;
            wasPurchased = true;

            ColorBlock cb = yo.colors;
            cb.normalColor = cb.selectedColor;
            yo.colors = cb;
        }
        else
        {
            CheckMoney();
        }

        

        //EventManager.Subscribe(Evento.EquipItem, OnOtherItemEquipped);
        EventManager.Subscribe(Evento.CoinUpdate, CheckMoney);
    }

    public void CheckMoney(params object[] parameters)
    {
        if (LevelManager.instance.allSkins[itemName] == 1)
        {
            return;
        }
        if (LevelManager.instance.Coins >= itemCost)
        {
            print("tenes guita. itemstate unlocked - button enabled");
            itemState = ItemState.Unlocked;
            yo.enabled = true;
            yo.interactable = true;

        }
        else
        {
            print("no tenes plata campeon. start shopitembutton - button disabled");
            yo.enabled = false;
            yo.interactable = false;
        }
    }


    public override void OnPointerUp(PointerEventData eventData)
    {
        switch (itemState)
        {
            case ItemState.Locked:
                print("no tenes guita para comprar esto");
                break;

            case ItemState.Unlocked:
                if (!wasPurchased)
                {
                    print("tuki, te compraste y equipaste " + itemName);
                    LevelManager.instance.Coins -= itemCost;
                    //EventManager.Trigger(Evento.UnequipItem);
                    EventManager.Trigger(Evento.EquipItemButtonUp, itemSprite);

                    wasPurchased = true;
                    ItemButtonsManager.instance.Purchase(itemName);
                    EventManager.Trigger(Evento.CoinUpdate, LevelManager.instance.Coins);
                }
                else
                {
                    print("volviste a equipar " + itemName);
                    //EventManager.Trigger(Evento.UnequipItem);
                    EventManager.Trigger(Evento.EquipItemButtonUp, itemSprite);
                }
                LevelManager.instance.SaveData();
                break;

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
