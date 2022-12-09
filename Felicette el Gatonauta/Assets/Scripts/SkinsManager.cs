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

    public Sprite defaultSkin;
    public Sprite currentSkin;

    //ya se me va a ocurrir como pedir esto sin depender de los botones
    [HideInInspector]
    public string[] skinNames = new string[4] { "Nave Golden", 
                                                "Nave Diamond", 
                                                "LANA-V", 
                                                "Ratonave"};

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
    }

    public void EquipSkin(params object[] parameters)
    {
        currentSkin = (Sprite)parameters[0];
        print("Skins manager: cambie el currentSkin a " + currentSkin);
    }

    
}
