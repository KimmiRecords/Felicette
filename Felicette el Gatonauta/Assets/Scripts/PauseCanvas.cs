using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCanvas : MonoBehaviour
{
    public GameObject pauseCanvas;

    void Start()
    {
        EventManager.Subscribe(Evento.PauseButtonUp, ShowPauseCanvas);
        EventManager.Subscribe(Evento.UnpauseButtonUp, HidePauseCanvas);
        EventManager.Subscribe(Evento.GoToSceneButtonUp, HidePauseCanvas);
    }

    public void ShowPauseCanvas(params object[] parameters)
    {
        AudioManager.instance.PlayByNamePitch("PickupSFX", 1.2f);
        pauseCanvas.SetActive(true);
        //print("showpausecanvas: muestro");
        Time.timeScale = 0;
    }

    public void HidePauseCanvas(params object[] parameters)
    {
        AudioManager.instance.PlayByNamePitch("PickupReversedSFX", 1.2f);
        pauseCanvas.SetActive(false);
        //print("hidepausecanvas: escondo");
        Time.timeScale = 1;
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded) //cuando se destruye porque lo destrui a mano
        {
            //print("destrui a este shipthrusters on isloaded");
        }
        else //cuando se destruye porque cambie de escena
        {
            EventManager.Unsubscribe(Evento.PauseButtonUp, ShowPauseCanvas);
            EventManager.Unsubscribe(Evento.UnpauseButtonUp, HidePauseCanvas);
            EventManager.Unsubscribe(Evento.GoToSceneButtonUp, HidePauseCanvas);
        }
    }
}
