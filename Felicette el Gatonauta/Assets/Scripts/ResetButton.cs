using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ResetButton : Button
{
    //Scene currentScene;

    private void Start()
    {
        //currentScene = SceneManager.GetActiveScene();
        EventManager.Subscribe("DeathWall", ResetScene);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("scene reset");
        ResetScene();
    }

    public void ResetScene(params object[] parameters)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
