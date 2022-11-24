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

    TMPro.TextMeshProUGUI myTMP;

    Button yo;
    public bool wasPurchased;
    ItemState itemState = ItemState.Locked;

    // Start is called before the first frame update
    void Start()
    {
        yo = GetComponent<Button>();
        myTMP = GetComponentInChildren<TMPro.TextMeshProUGUI>();

        if (!LevelManager.instance.allSkins.ContainsKey(itemName))
        {
            ItemButtonsManager.instance.AddButton(itemName);
        }

        if (LevelManager.instance.allSkins[itemName] == 1)
        {
            //print este item ya estaba comprado
            //print("start shopitembutton - este item ya estaba comprado");
            itemState = ItemState.Unlocked;
            yo.enabled = true;
            yo.interactable = true;
            wasPurchased = true;
            myTMP.text = itemName;

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
        if (LevelManager.instance.allSkins[itemName] == 1)
        {
            return;
        }
        if (LevelManager.instance.Coins >= itemCost)
        {
            //print("tenes guita. itemstate unlocked - button enabled");
            itemState = ItemState.Unlocked;
            yo.enabled = true;
            yo.interactable = true;
            
        }
        else
        {
            //print("no tenes plata campeon. start shopitembutton - button disabled");
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
                    //print("tuki, te compraste y equipaste " + itemName);
                    AudioManager.instance.PlayByName("PurchaseItem");
                    AudioManager.instance.PlayByName("EquipItem");
                    EventManager.Trigger(Evento.EquipItemButtonUp, itemSprite, this);
                    wasPurchased = true;
                    ItemButtonsManager.instance.Purchase(itemName);
                    myTMP.text = itemName;
                    LevelManager.instance.Coins -= itemCost;
                }
                else
                {
                    //print("volviste a equipar " + itemName);
                    AudioManager.instance.PlayByName("EquipItem");
                    EventManager.Trigger(Evento.EquipItemButtonUp, itemSprite, this);
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
