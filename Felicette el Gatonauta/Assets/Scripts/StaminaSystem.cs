using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class StaminaSystem : MonoBehaviour
{
    [SerializeField] float timeToRecharge = 10f;
    bool restoring;

    public bool HaveStamina { get => LevelManager.instance.Stamina > 0; }

    DateTime nextStaminaTime;
    DateTime lastStaminaTime;

    [SerializeField] TextMeshProUGUI timerText = null;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("stamina"))
        {
            PlayerPrefs.SetInt("stamina", LevelManager.instance.maxStamina);
        }
        LoadTime();
        StartCoroutine(RestoreEnergy());

    }

    

    IEnumerator RestoreEnergy()
    {
        UpdateStamina();
        restoring = true;
        while (LevelManager.instance.Stamina < LevelManager.instance.maxStamina)
        {
            DateTime currentDateTime = DateTime.Now;
            DateTime nextDateTime = nextStaminaTime;
            bool staminaAdd = false;

            while (currentDateTime > nextDateTime)
            {
                if(LevelManager.instance.Stamina < LevelManager.instance.maxStamina)
                {
                    LevelManager.instance.Stamina += 1;
                    staminaAdd = true;
                    UpdateStamina();
                    DateTime timeToAdd = DateTime.Now;
                    if (lastStaminaTime > nextDateTime)
                        timeToAdd = lastStaminaTime;
                    else
                        timeToAdd = nextDateTime;

                    nextDateTime = AddDuration(timeToAdd, timeToRecharge);
                }
                else
                {
                    break;
                }
            }

            if (staminaAdd)
            {
                lastStaminaTime = DateTime.Now;
                nextStaminaTime = nextDateTime;
            }

            UpdateTimer();
            UpdateStamina();
            SaveTime();
            yield return new WaitForEndOfFrame();
        }

        restoring = false;
    }

    DateTime AddDuration(DateTime date, float duration)
    {
        return date.AddSeconds(duration);
    }

    public void UseEnergy(int energyAmmount)
    {
        if (LevelManager.instance.Stamina - energyAmmount >= 0)
        {
            LevelManager.instance.Stamina -= energyAmmount;
            //UpdateStamina();

            if (!restoring)
            {
                nextStaminaTime = AddDuration(DateTime.Now, timeToRecharge);
                StartCoroutine(RestoreEnergy());
            }
        }
        else
        {
            Debug.Log("No tenés stamina!!!!");
        }
    }

    void UpdateStamina()
    {
        EventManager.Trigger(Evento.StaminaUpdate, LevelManager.instance.Stamina);
        //staminaText.text = staminaAmmount.ToString() + " / " + maxStamina.ToString();
    }

    void UpdateTimer()
    {
        if(LevelManager.instance.Stamina >= LevelManager.instance.maxStamina)
        {
            timerText.text = "";
            //  print("tengo mas stamina que el max");
            return;
        }

        TimeSpan timer = nextStaminaTime - DateTime.Now;
        timerText.text = timer.Minutes.ToString() + ":" + timer.Seconds.ToString();
    }

    void LoadTime()
    {
        LevelManager.instance.Stamina = PlayerPrefs.GetInt("stamina");
        nextStaminaTime = StringToDateTime(PlayerPrefs.GetString("nextStaminaTime"));
        lastStaminaTime = StringToDateTime(PlayerPrefs.GetString("lastStaminaTime"));
    }

    void SaveTime()
    {
        PlayerPrefs.SetInt("stamina", LevelManager.instance.Stamina);
        PlayerPrefs.SetString("nextStaminaTime", nextStaminaTime.ToString());
        PlayerPrefs.SetString("lastStaminaTime", lastStaminaTime.ToString());
    }

    DateTime StringToDateTime(string timeString)
    {
        if (string.IsNullOrEmpty(timeString))
        {
            return DateTime.Now;
        }
        else
        {
            return DateTime.Parse(timeString);
        }
    }
}
