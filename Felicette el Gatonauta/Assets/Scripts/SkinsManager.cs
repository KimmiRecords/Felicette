using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct ShipSkin
{
    public Sprite sprite;
    public string name;
    public int cost;
}

public class SkinsManager : MonoBehaviour
{
    public static SkinsManager instance;

    //el bool es si fue comprado o no
    //public Dictionary<string, Sprite> allButtons = new Dictionary<string, bool>();

    public Sprite currentSkin;



    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }


    void Start()
    {
        EventManager.Subscribe(Evento.EquipItemButtonUp, EquipSkin);
        //currentSkin = null;

        //eventualmente esto tiene un diccionario que une strings a sprites, asi aca en el codigo 
        //puedo escribir el nombre de lo que quiero, y no el Sprite. como con el audiomanager
    }

    public void EquipSkin(params object[] parameters)
    {
        currentSkin = (Sprite)parameters[0];
        print("Skins manager: cambie el currentSkin a " + currentSkin);
    }
}
