using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    void Start()
    {
        EventManager.Subscribe("WinWall", GoToScene);
    }

    void GoToScene(params object[] parameters)
    {
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
}
