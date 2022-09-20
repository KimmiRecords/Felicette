using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public int nivelesJugables;
    bool[] nivelesCompletados;

    string escenaEnLaQuePerdiYVoyAResetearSiTocoReiniciarNivel;

    int _coins;

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
    }
    void Start()
    {
        AudioManager.instance.PlayBGM();

        //eventos de los niveles
        EventManager.Subscribe(Evento.WinWall, NivelCompletado);
        EventManager.Subscribe(Evento.DeathWall, NivelFallado);
        EventManager.Subscribe(Evento.CoinPickup, AddCoin);
        EventManager.Subscribe(Evento.ExitLevelButtonUp, ExitLevel);

        //btones de menuses
        EventManager.Subscribe(Evento.QuitGameButtonUp, QuitGame);
        EventManager.Subscribe(Evento.GoToSceneButtonUp, GoToScene);
        EventManager.Subscribe(Evento.EraseDataButtonUp, EraseData);

        //???
        EventManager.Subscribe(Evento.ResetLevelButtonUp, ResetLevel);

        nivelesCompletados = new bool[nivelesJugables];
        //print("LEVEL MANAGER: hay " + nivelesCompletados.Length + " niveles");
        //print("PlayerPrefs: hay " + PlayerPrefs.GetInt("nivelesCompletados") + " niveles completados");
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        _coins++;
    //        print("toque esc");
    //        print("tengo " + _coins + " coins");
    //    }

    //    if (Input.GetKeyDown(KeyCode.Q))
    //    {
    //        SaveData();
    //        print("toque Q");
    //    }

    //    if (Input.GetKeyDown(KeyCode.W))
    //    {
    //        LoadData();
    //        print("toque W");
    //    }
    //}

    public void SaveData()
    {
        PlayerPrefs.SetInt("coins", _coins);
        PlayerPrefs.SetInt("nivelesCompletados", CountCompletedLevels(nivelesCompletados));
        PlayerPrefs.Save();

        //print("guarde la data");
    }

    public void LoadData()
    {
        Coins = PlayerPrefs.GetInt("coins");

        for (int i = 0; i < PlayerPrefs.GetInt("nivelesCompletados"); i++)
        {
            nivelesCompletados[i] = true;
        }

        EventManager.Trigger(Evento.CoinUpdate, Coins);

        //print("cargue la data");
        //print("load data: tengo " + Coins + " coins");
        //print("load data: hay " + CountCompletedLevels(nivelesCompletados) + " niveles completados");
    }

    public void EraseData(params object[] parameters)
    {
        PlayerPrefs.SetInt("coins", 0);
        PlayerPrefs.SetInt("nivelesCompletados", 0);
        LoadData();

        //print("erase data: tengo " + Coins + " coins");
        //print("erase data: hay " + CountCompletedLevels(nivelesCompletados) + " niveles completados");
    }

    public int CountCompletedLevels(bool[] levelList)
    {
        int value = 0;

        for (int i = 0; i < levelList.Length; i++)
        {
            if (levelList[i] == true)
            {
                value++;
            }
        }
        print("en " + levelList + " hay " + value + " valores True");
        return value;
    }

    public void AddCoin(params object[] parameters)
    {
        _coins++;
        //print("ahora tengo " + _coins + " coins");
        EventManager.Trigger(Evento.CoinUpdate, _coins);
    }

    void GoToScene(params object[] parameters)
    {
        AudioManager.instance.StopByName("RadioPreLaunchSFX");
        AudioManager.instance.StopByName("PropulsoresSFX");

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
            //print("el primer parametro que me pasaste no era un string");
        }
    }

    void NivelCompletado(params object[] parameters)
    {
        //pongo true en el numero de nivel que se completo recien
        if (parameters[1] is int)
        {
            nivelesCompletados[(int)parameters[1]] = true;
            print("acabo de completar el nivel " + (int)parameters[1]);

        }
        else
        {
            print("el primer parametro que me pasaste no era un int");
        }

        SaveData();
        print("nivel completado: savedata");

        GoToScene("LevelComplete");
    }

    public void NivelFallado(params object[] parameters)
    {
        escenaEnLaQuePerdiYVoyAResetearSiTocoReiniciarNivel = (string)parameters[0];
        LoadData();
        print("nivel fallado: loaddata");

        GoToScene("LevelFailed");
    }

    public void ExitLevel(params object[] parameters)
    {
        LoadData();
        print("exitlevel: loaddata");
    }

    public void ResetLevel(params object[] parameters)
    {
        GoToScene(escenaEnLaQuePerdiYVoyAResetearSiTocoReiniciarNivel);
    }

    public void ResetScene(params object[] parameters)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame(params object[] parameters)
    {
        Application.Quit();
        print("quitee el juego");
    }

}
