using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI monedasGanadasTMP;

    void Start()
    {
        //mandar el ad interstitial molesto

        int monedas = Random.Range(5, 11) + PlayerPrefs.GetInt("nivelesCompletados");
        LevelManager.instance.Coins += monedas;
        AudioManager.instance.PlayByNamePitch("CoinRain", 1.1f);


        monedasGanadasTMP.text = "Ganaste " + monedas + " monedas extra!";
        LevelManager.instance.SaveData();
    }
}
