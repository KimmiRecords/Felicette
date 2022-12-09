using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarezas
{
    raro,
    ultrararo,
    legendario
}

public class GachaManager : ShopItemButton
{
    //con mis arrays de items ordenados por rareza, gg

    public Sprite[] raros;
    public Sprite[] ultrararos;
    public Sprite[] legendarios;

    int rareza = 0;

    void Start()
    {
        EventManager.Subscribe(Evento.GachaButtonUp, StartGacha);
    }


    public void StartGacha(params object[] parameters)
    {
        //crear el random
        //dar el sprite que sea
        //avisar a los demas para que se actualice en todos lados
        SetRandomValue();


        switch (rareza)
        {
            case 0:
                print("no ganaste nada");
                break;
            case 1:
                print("ganaste una nave rara");
                break;

            case 2:
                print("ganaste una nave ultra rara");
                break;

            case 3:
                print("ganaste una nave legendaria");
                break;
        }
    }

    public override void PopupConfirm(params object[] parameters)
    {
        base.PopupConfirm(parameters);
    }
    
    public void SetRandomValue()
    {
        float random = Random.Range(0f, 100f);

        if (random < 1)
        {
            rareza = 3;
        }
        else if (random < 5)
        {
            rareza = 2;
        }
        else if (random < 10)
        {
            rareza = 1;
        }
        else
        {
            rareza = 0;
        }
    }

    public void GetItem()
    {
        if (LevelManager.instance.allSkins[itemData.name] == 1)
        {
            //no pasa nada
        }
        else
        {
            print("tuki item nuevo");
            AudioManager.instance.PlayByName("PurchaseItem");
            AudioManager.instance.PlayByName("EquipItem");
            EventManager.Trigger(Evento.EquipItemButtonUp, itemData.sprite, this);
            wasPurchased = true;
            ItemButtonsManager.instance.Purchase(itemData.name);
            myTMP.text = itemData.name;
            LevelManager.instance.Coins -= itemData.cost;
        }
    }
}
