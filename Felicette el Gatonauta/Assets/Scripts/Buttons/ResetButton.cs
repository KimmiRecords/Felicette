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
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        //Debug.Log("scene reset");
        EventManager.Trigger("ResetButtonUp");
    }

   
}
