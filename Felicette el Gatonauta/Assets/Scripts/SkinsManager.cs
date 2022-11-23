using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        currentSkin = null;
    }

    public void EquipSkin(params object[] parameters)
    {
        currentSkin = (Sprite)parameters[0];
        print("Skins manager: cambie el currentSkin a " + currentSkin);
    }
}
