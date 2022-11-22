using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI monedasGanadasTMP;

    void Start()
    {
        int monedas = Random.Range(5, 11) + PlayerPrefs.GetInt("nivelesCompletados");

        for (int i = 0; i < monedas; i++)
        {
            LevelManager.instance.AddCoin();
        }
        monedasGanadasTMP.text = "Ganaste " + monedas + " monedas extra!";
        LevelManager.instance.SaveData();
    }
}
