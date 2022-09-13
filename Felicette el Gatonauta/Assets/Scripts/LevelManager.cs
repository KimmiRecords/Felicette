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
        //SceneManager.sceneLoaded += OnSceneLoaded;

        //eventos de los niveles
        EventManager.Subscribe(Evento.WinWall, NivelCompletado);
        EventManager.Subscribe(Evento.DeathWall, NivelFallado);

        //btones del main menu
        EventManager.Subscribe(Evento.QuitGameButtonUp, QuitGame);

        //botones de GoToScene
        EventManager.Subscribe(Evento.GoToSceneButtonUp, GoToScene);
        EventManager.Subscribe(Evento.ResetLevelButtonUp, ResetLevel);




        nivelesCompletados = new bool[nivelesJugables];
        print("hay " + nivelesCompletados.Length + " niveles");
        for (int i = 0; i < nivelesCompletados.Length; i++)
        {
            nivelesCompletados[i] = false;
            print("el nivel" + i + " es " + nivelesCompletados[i]);
        }
    }

    void GoToScene(params object[] parameters)
    {
        //al metodo gotoscene le pasas un string con el nombre de la escena a la que queres ir
        if (parameters[0] is string)
        {
            //print(parameters[0]);
            string sceneName = parameters[0].ToString();
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
        if (parameters[0] is int)
        {
            nivelesCompletados[(int)parameters[0]] = true;
            print("acabo de completar el nivel " + (int)parameters[0]);
        }
        else
        {
            print("el primer parametro que me pasaste no era un int");
        }

        GoToScene("LevelComplete");
    }

    public void NivelFallado(params object[] parameters)
    {
        escenaEnLaQuePerdiYVoyAResetearSiTocoReiniciarNivel = (string)parameters[0];
        GoToScene("LevelFailed");
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

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Scene ultimoNivelCargado;

        //ultimaEscenaCargada = SceneManager.GetActiveScene();

        //Debug.Log("OnSceneLoaded: " + scene.name);
        //Debug.Log(mode);
    }
}
