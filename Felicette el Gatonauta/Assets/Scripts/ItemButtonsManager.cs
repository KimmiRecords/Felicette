using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemButtonsManager : MonoBehaviour
{
    public static ItemButtonsManager instance;

    

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

    private void Start()
    {
        LevelManager.instance.LoadData();
    }

    public void AddButton(string b)
    {
        //cada button dispara este metodo para autoagregarse
        if (!LevelManager.instance.allSkins.ContainsKey(b))
        {
            print("agregue a " + b + " al dict");
            LevelManager.instance.allSkins.Add(b, 0);
        }
        else
        {
            print("este dict ya contiene a " + b);
        }
    }

    public void Purchase(string b)
    {
        print("itembutton manager: purchased " + b);
        //en el dict, cambio el valor de esa entrada a 1, que es Comprado.
        LevelManager.instance.allSkins[b] = 1;

    }
}
