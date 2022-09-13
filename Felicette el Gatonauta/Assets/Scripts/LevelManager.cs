using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

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
        //cosas del nivel
        EventManager.Subscribe("WinWall", GoToScene);
        EventManager.Subscribe("DeathWall", ResetScene);
        EventManager.Subscribe("ExitLevelButtonUp", GoToScene);

        //btones del main menu
        EventManager.Subscribe("PlayButtonUp", GoToScene);
        EventManager.Subscribe("SettingsButtonUp", GoToScene);
        EventManager.Subscribe("QuitGameButtonUp", QuitGame);

        //botones de settings
        EventManager.Subscribe("BackToMainMenuButtonUp", GoToScene);

        //botones del level select
        EventManager.Subscribe("LevelSelectButtonUp", GoToScene);



    }

    void GoToScene(params object[] parameters)
    {
        //al metodo gotoscene le pasas un string con el nombre de la escena a la que queres ir
        if (parameters[0] is string)
        {
            print(parameters[0]);
            string sceneName = parameters[0].ToString();
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            print("el primer parametro que me pasaste no era un string");
        }
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
