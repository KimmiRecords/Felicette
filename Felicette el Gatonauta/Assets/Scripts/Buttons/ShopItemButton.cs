using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public enum ItemState
{
    Locked,
    Unlocked,
    Purchased,
    Equipped
}
public class ShopItemButton : BaseButton
{
    public string itemName;
    public int itemCost;
    public Sprite itemSprite;

    public Color lockedColor;
    public Color unlockedColor;
    public Color purchasedColor;
    public Color equippedColor;

    Button yo;


    ItemState itemState = ItemState.Locked;

    // Start is called before the first frame update
    void Start()
    {
        yo = GetComponent<Button>();
        //SetColor(lockedColor);
        //yo.enabled = false;

        if (LevelManager.instance.Coins > itemCost)
        {
            itemState = ItemState.Unlocked;
            //SetColor(unlockedColor);
            //yo.enabled = true;

        }

        EventManager.Subscribe(Evento.EquipItem, OnOtherItemEquipped);
        EventManager.Subscribe(Evento.CoinUpdate, UpdateCoinAmount);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnOtherItemEquipped(params object[] parameters)
    {
        if (itemSprite != (Sprite)parameters[0])
        {
            //si el sprite que quiero cargar es disinto, significa que estoy equipando otro item. entonces des-equipo este.
            if (itemState == ItemState.Equipped)
            {
                itemState = ItemState.Purchased;
                EventManager.Trigger(Evento.UnequipItem);
                print("des-equipo este item");
            }
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
                print("tuki, te compraste " + itemName);
                itemState = ItemState.Purchased;
                LevelManager.instance.Coins -= itemCost;
                EventManager.Trigger(Evento.CoinUpdate, LevelManager.instance.Coins);
                //UpdateCoinAmount();
                //SetColor(purchasedColor);
                
                break;

            case ItemState.Purchased:
                print("equipaste este item");
                EventManager.Trigger(Evento.UnequipItem);

                EventManager.Trigger(Evento.EquipItem, itemSprite);
                itemState = ItemState.Equipped;
                //SetColor(equippedColor);



                break;

            case ItemState.Equipped:
                print("te des-equipaste este item");
                itemState = ItemState.Purchased;
                EventManager.Trigger(Evento.UnequipItem);
                //SetColor(purchasedColor);

                break;
        }
    }

    public void SetColor(Color c)
    {
        ColorBlock cb = yo.colors;
        cb.normalColor = c;
        cb.selectedColor = c;

        yo.colors = cb;
    }

    public void UpdateCoinAmount(params object[] parameters)
    {
        if ((int)parameters[0] < itemCost)
        {
            if (itemState == ItemState.Unlocked)
            {
                //si ahora mi guita es menor al costo, apago el boton
                //SetColor(lockedColor);
                yo.enabled = false;
            }

        }
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            //print("destrui a este  on isloaded");
        }
        else
        {
            EventManager.Unsubscribe(Evento.EquipItem, OnOtherItemEquipped);
            EventManager.Unsubscribe(Evento.CoinUpdate, UpdateCoinAmount);

            //print("destrui a este  on sceneclosure");
        }
    }
}
