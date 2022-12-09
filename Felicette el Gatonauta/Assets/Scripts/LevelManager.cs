using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //un singleton que lleva la cuenta de tus monedas y los niveles completados
    //tiene los metodos para guardar y moverte entre escenas

    public static LevelManager instance;

    public int nivelesJugables;
    public int maxStamina;

    bool[] _nivelesCompletados;
    string _sceneToRestart;
    int _coins;
    int _stamina;
    
    [HideInInspector]
    public bool inDeathSequence = false;

    //el int 0 = comprado. int 1 = no comprado
    public Dictionary<string, int> allSkins = new Dictionary<string, int>();

    public Canvas canvas; //mi lmcanvas
    public Image currentSkinImage;

    public StaminaSystem myStaminaSystem;
    public NotificationManager myNotificationManager;

    public int Coins
    {
        get
        {
            return _coins;
        }
        set
        {
            _coins = value;

            if (_coins < 0)
            {
                _coins = 0;
            }
            EventManager.Trigger(Evento.CoinUpdate, _coins);

        }
    }
    public int Stamina
    {
        get
        {
            return _stamina;
        }
        set
        {
            _stamina = value;

            if (_stamina < 0)
            {
                _stamina = 0;
            }

            if (_stamina > maxStamina)
            {
                _stamina = maxStamina;
            }
            EventManager.Trigger(Evento.StaminaUpdate, _stamina);
        }
    }

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

        Screen.orientation = ScreenOrientation.Portrait;

        _nivelesCompletados = new bool[nivelesJugables];

    }
    void Start()
    {
        AudioManager.instance.PlayBGM();

        //eventos de los niveles
        EventManager.Subscribe(Evento.WinWall, NivelCompletado);
        EventManager.Subscribe(Evento.DeathWall, NivelFallado);
        EventManager.Subscribe(Evento.CoinPickup, AddCoin);
        EventManager.Subscribe(Evento.ResetLevelButtonUp, ResetLevel);
        EventManager.Subscribe(Evento.ExitLevelButtonUp, ExitLevel);

        //btones de menuses
        EventManager.Subscribe(Evento.QuitGameButtonUp, QuitGame);
        EventManager.Subscribe(Evento.GoToSceneButtonUp, GoToScene);
        EventManager.Subscribe(Evento.EraseDataButtonUp, EraseData);
        EventManager.Subscribe(Evento.SplashButtonUp, ActivateLMCanvas);
        EventManager.Subscribe(Evento.EquipItemButtonUp, SetSkinImage);

        if (SceneManager.GetActiveScene().name == "Splash")
        {
            canvas.gameObject.SetActive(false);
        }

        LoadData();
        //print("LEVEL MANAGER: hay " + nivelesCompletados.Length + " niveles");
        //print("PlayerPrefs: hay " + PlayerPrefs.GetInt("nivelesCompletados") + " niveles completados");
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            AddCoin();
        }
    }
    public void SaveData()
    {
        PlayerPrefs.SetInt("coins", _coins);
        PlayerPrefs.SetInt("stamina", _stamina);
        PlayerPrefs.SetInt("nivelesCompletados", CountCompletedLevels(_nivelesCompletados));

        if (allSkins.Count > 0)
        {
            foreach (string skinName in SkinsManager.instance.skinNames)
            {
                PlayerPrefs.SetInt(skinName, allSkins[skinName]);
                //print("guarde el" + skinName.Key + " con " + allSkins[skinName.Key]);
            }
        }
        PlayerPrefs.Save();
    }
    public void LoadData()
    {
        Coins = PlayerPrefs.GetInt("coins");
        EventManager.Trigger(Evento.CoinUpdate, Coins);

        Stamina = PlayerPrefs.GetInt("stamina");
        EventManager.Trigger(Evento.StaminaUpdate, Stamina);

        for (int i = 0; i < PlayerPrefs.GetInt("nivelesCompletados"); i++)
        {
            _nivelesCompletados[i] = true;
        }

        if (allSkins.Count > 0)
        {
            foreach (string skinName in SkinsManager.instance.skinNames)
            {
                //allSkins[skinName.Key] = PlayerPrefs.GetInt(skinName.Key);
                SetDictionaryKeyAndValue(skinName, PlayerPrefs.GetInt(skinName));
                //print("cargue el " + skinName.Key + " con " + PlayerPrefs.GetInt(skinName.Key));
            }
        }
    }
    public void EraseData(params object[] parameters)
    {
        PlayerPrefs.SetInt("coins", 0);
        PlayerPrefs.SetInt("stamina", 50);
        PlayerPrefs.SetInt("nivelesCompletados", 0);
        //print("erase: en allskins hay " + allSkins.Count + " elementos");
        if (SkinsManager.instance != null)
        {
            EventManager.Trigger(Evento.EquipItemButtonUp, SkinsManager.instance.defaultSkin);
        }

        allSkins.Clear();
        LoadData();
    }
    public int CountCompletedLevels(bool[] completedLevels)
    {
        int value = 0;

        for (int i = 0; i < completedLevels.Length; i++)
        {
            if (completedLevels[i] == true)
            {
                value++;
            }
        }
        //print("en " + levelList + " hay " + value + " valores True");
        return value;
    }
    public void SetDictionaryKeyAndValue(string key, int value)
    {
        if (allSkins.ContainsKey(key))
        {
            allSkins[key] = value;
        }
    }
    public void AddCoin(params object[] parameters)
    {
        Coins++;
        AudioManager.instance.PlayByNamePitch("PickupSFX", 1.9f);

    }
    void GoToScene(params object[] parameters)
    {
        AudioManager.instance.StopByName("RadioPreLaunchSFX");
        AudioManager.instance.StopByName("PropulsoresSFX");
        AudioManager.instance.StopByName("GravityAoE");
        AudioManager.instance.StopByName("TimerFourTicks");

        if (parameters[0] is string)
        {
            string sceneName = parameters[0].ToString();

            if (sceneName == "SampleScene")
            {
                AudioManager.instance.PlayByName("RadioPreLaunchSFX");
            }

            switch ((string)(parameters[0]))
            {
                case "Tienda":
                    AudioManager.instance.StopByName("SpringWaltzLoop");
                    AudioManager.instance.StopByName("EroicaLoop");
                    AudioManager.instance.PlayByName("PetSocShop");
                    break;
                case "MainMenu":
                    if (SceneManager.GetActiveScene().name == "Tienda" ||
                        SceneManager.GetActiveScene().name == "Gacha")
                    {
                        AudioManager.instance.StopByName("PetSocShop");
                        AudioManager.instance.PlayByName("SpringWaltzLoop");
                    }
                    break;
                case "Tutorial":
                    AudioManager.instance.StopByName("SpringWaltzLoop");
                    AudioManager.instance.PlayByName("RadioPreLaunchSFX");
                    break;
                case "SampleScene":
                    AudioManager.instance.StopByName("SpringWaltzLoop");
                    AudioManager.instance.PlayByName("RadioPreLaunchSFX");
                    break;
                case "Nivel2":
                    AudioManager.instance.StopByName("SpringWaltzLoop");
                    AudioManager.instance.PlayByName("RadioPreLaunchSFX");
                    break;
                case "Nivel3":
                    AudioManager.instance.StopByName("SpringWaltzLoop");
                    AudioManager.instance.PlayByName("RadioPreLaunchSFX");
                    break;
                case "LevelSelect":
                    if (!AudioManager.instance.sound["EroicaLoop"].isPlaying &&
                        !AudioManager.instance.sound["SpringWaltzLoop"].isPlaying)
                    {
                        AudioManager.instance.PlayByName("SpringWaltzLoop");
                    }
                    break;
                case "Gacha":
                    AudioManager.instance.StopByName("SpringWaltzLoop");
                    AudioManager.instance.StopByName("EroicaLoop");
                    AudioManager.instance.PlayByName("PetSocShop");
                    break;
                default:
                    break;
            }


            SceneManager.LoadScene(sceneName);
        }
        else
        {
            print("el primer parametro que me pasaste no era un string");
        }
    }
    void NivelCompletado(params object[] parameters)
    {
        //pongo true en el numero de nivel que se completo recien
        //y te mando a la escena de level completed

        if (parameters[1] is int)
        {
            _nivelesCompletados[(int)parameters[1]] = true;
            //print("acabo de completar el nivel " + (int)parameters[1]);
        }
        else
        {
            //print("el primer parametro que me pasaste no era un int");
        }

        SaveData();
        //print("nivel " + (int)parameters[1] + " completado: savedata");

        GoToScene("LevelComplete");
    }
    public void NivelFallado(params object[] parameters)
    {
        print("LM nivel fallado");
        _sceneToRestart = (string)parameters[0];
        LoadData();
        GoToScene("LevelFailed");
    }
    public void ExitLevel(params object[] parameters)
    {
        LoadData();
        //print("exitlevel: loaddata");
    }
    public void ResetLevel(params object[] parameters)
    {
        GoToScene(_sceneToRestart);
    }
    public void QuitGame(params object[] parameters)
    {
        myNotificationManager.PrepareNotification();
        Application.Quit();
        print("quitee el juego");
    }
    public void ActivateLMCanvas(params object[] parameters)
    {
        canvas.gameObject.SetActive(true);
    }
    public void SetSkinImage(params object[] parameters)
    {
        currentSkinImage.sprite = (Sprite)parameters[0];
    }

}
