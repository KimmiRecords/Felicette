using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //un singleton que lleva la cuenta de tus monedas y los niveles completados
    //tiene los metodos para guardar y moverte entre escenas

    public static LevelManager instance;

    public int nivelesJugables;

    bool[] _nivelesCompletados;
    string _sceneToRestart;
    int _coins;

    //el bool es si fue comprado o no
    public Dictionary<string, int> allSkins = new Dictionary<string, int>();

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
       
        _nivelesCompletados = new bool[nivelesJugables];

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
        PlayerPrefs.SetInt("nivelesCompletados", CountCompletedLevels(_nivelesCompletados));
        PlayerPrefs.SetInt("pirata", allSkins["pirata"]);
        PlayerPrefs.SetInt("nonla", allSkins["nonla"]);

        PlayerPrefs.Save();

        //print("guarde la data");
    }
    public void LoadData()
    {
        Coins = PlayerPrefs.GetInt("coins");
        EventManager.Trigger(Evento.CoinUpdate, Coins);

        for (int i = 0; i < PlayerPrefs.GetInt("nivelesCompletados"); i++)
        {
            _nivelesCompletados[i] = true;
        }

        allSkins["pirata"] = PlayerPrefs.GetInt("pirata");
        allSkins["nonla"] = PlayerPrefs.GetInt("nonla");



        //print("cargue la data");
    }
    public void EraseData(params object[] parameters)
    {
        PlayerPrefs.SetInt("coins", 0);
        PlayerPrefs.SetInt("nivelesCompletados", 0);
        PlayerPrefs.SetInt("pirata", 0);
        PlayerPrefs.SetInt("nonla", 0);

        LoadData();

        //print("erase data: tengo " + Coins + " coins");
        //print("erase data: hay " + CountCompletedLevels(nivelesCompletados) + " niveles completados");
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

    public void AddCoin(params object[] parameters)
    {
        _coins++;
        EventManager.Trigger(Evento.CoinUpdate, _coins);
    }
    void GoToScene(params object[] parameters)
    {
        AudioManager.instance.StopByName("RadioPreLaunchSFX");
        AudioManager.instance.StopByName("PropulsoresSFX");
        AudioManager.instance.StopByName("GravityAoE");
        AudioManager.instance.StopByName("TimerFourTicks");



        //al metodo gotoscene le pasas un string con el nombre de la escena a la que queres ir
        if (parameters[0] is string)
        {
            string sceneName = parameters[0].ToString();

            if (sceneName == "SampleScene")
            {
                AudioManager.instance.PlayByName("RadioPreLaunchSFX");
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
            print("acabo de completar el nivel " + (int)parameters[1]);
        }
        else
        {
            print("el primer parametro que me pasaste no era un int");
        }

        SaveData();
        //print("nivel " + (int)parameters[1] + " completado: savedata");

        GoToScene("LevelComplete");
    }
    public void NivelFallado(params object[] parameters)
    {
        _sceneToRestart = (string)parameters[0];
        //print(_escenaEnLaQuePerdiYVoyAResetearSiTocoReiniciarNivel);
        LoadData();
        //print("nivel fallado: loaddata");

        GoToScene("LevelFailed");
    }
    public void ExitLevel(params object[] parameters)
    {
        LoadData();
        print("exitlevel: loaddata");
    }
    public void ResetLevel(params object[] parameters)
    {
        GoToScene(_sceneToRestart);
    }
    public void QuitGame(params object[] parameters)
    {
        Application.Quit();
        print("quitee el juego");
    }

}
