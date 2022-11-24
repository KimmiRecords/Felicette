using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public enum RewardType
{
    coins,
    stamina
}

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] string gameID = "5034173";
    string adToShow = "Rewarded_Android";
    RewardType adRewardType;

    public float staminaReward;

    private void Start()
    {
        Advertisement.AddListener(this);
        Debug.Log("agregue este admanager como listener");
        Advertisement.Initialize(gameID);
        Debug.Log("initialize");

        EventManager.Subscribe(Evento.WatchAdButtonUp, SetAdType);

    }

    public void SetAdType(params object[] parameters)
    {
        adToShow = (string)parameters[0];
        adRewardType = (RewardType)parameters[1];
        PlayAd();
    }

    public void PlayAd(params object[] parameters)
    {
        if (!Advertisement.IsReady())
        {
            print("PlayAd: quise darle play pero no estaba ready");
            return;
        }

        Advertisement.Show(adToShow);
        Debug.Log("PlayAd: muestro el " + adToShow);

    }

    public void OnUnityAdsReady(string placementId)
    {
        //Debug.Log("Ads Ready");
    }

    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == "Rewarded_Android")
        {
            if(ShowResult.Finished == showResult)
            {
                Debug.Log("Terminaste el ad: Te doy la recompensa");
                GiveReward();
            }
            else
            {
                Debug.Log("fallo o skipeaste, no te doy nada");
            }
        }
    }

                
    public void GiveReward()
    {

        if (adRewardType == RewardType.coins)
        {
            int randomCoins = Random.Range(8, 15);
            LevelManager.instance.Coins += randomCoins;
            print("te ganaste " + randomCoins + " monedas por ver ese horrible ad");
            AudioManager.instance.PlayByName("CoinRain");
        }
        else
        {
            LevelManager.instance.Stamina += staminaReward;
            print("te ganaste " + staminaReward + " stamina por ver ese horrible ad");
            AudioManager.instance.PlayByNamePitch("CoinRain", 0.5f);
        }
        LevelManager.instance.SaveData();



    }

    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded)
        {
            EventManager.Unsubscribe(Evento.WatchAdButtonUp, SetAdType);
        }
    }

    
}
