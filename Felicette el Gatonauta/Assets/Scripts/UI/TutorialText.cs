using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMPro.TextMeshProUGUI))]
public class TutorialText : MonoBehaviour
{
    //este script maneja la secuencia de textos que aparecen en el nivel tutorial

    TMPro.TextMeshProUGUI _yo;
    Color _originalColor;
    int _textIndex;

    public float fadeTime;
    public string[] textos;
    public Graphic[] cuadrosRojos;

    
    void Start()
    {
        _yo = GetComponent<TMPro.TextMeshProUGUI>();
        _originalColor = _yo.color;
        _textIndex = 0;
        _yo.text = textos[_textIndex];

        EventManager.Subscribe(Evento.BasePositionUp, BasePositionNext);
        EventManager.Subscribe(Evento.ThrusterDown, ThrusterNext);
        EventManager.Subscribe(Evento.NextTextWall, WallNext);
        EventManager.Subscribe(Evento.CajaPickup, CajaPickupNext);
        EventManager.Subscribe(Evento.PowerUpButtonUp, PowerUpNext);

        StartCoroutine(NextText());
    }

    //estos metodos son para prevenir que se sobre disparen los textos
    public void BasePositionNext(params object[] parameters)
    {
        if (_textIndex == 2)
        {
            StartCoroutine(NextText());
        }
        else
        {
            return;
        }
    }
    public void ThrusterNext(params object[] parameters)
    {
        if (_textIndex == 3)
        {
            StartCoroutine(NextText());
        }
        else
        {
            return;
        }
    }
    public void WallNext(params object[] parameters)
    {
        if (parameters[0] is int)
        {
            if (_textIndex == (int)parameters[0])
            {
                StartCoroutine(NextText());
            }
            else
            {
                return;
            }
        }
        else
        {
            print("ojo q no me pasaste un int");
        }
    }
    public void CajaPickupNext(params object[] parameters)
    {
        if (_textIndex == 7)
        {
            StartCoroutine(NextText());
        }
        else
        {
            return;
        }
    }
    public void PowerUpNext(params object[] parameters)
    {
        if (_textIndex == 8)
        {
            StartCoroutine(NextText());
        }
        else
        {
            return;
        }
    }

    public IEnumerator FadeOutText()
    {
        float elapsedTime = 0;

        while (elapsedTime < fadeTime)
        {
            _yo.color = Color.Lerp(_yo.color, Color.clear, elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _yo.color = Color.clear;
        yield return null;
    }
    public IEnumerator FadeInText()
    {
        float elapsedTime = 0;

        while (elapsedTime < fadeTime)
        {
            _yo.color = Color.Lerp(Color.clear, _originalColor, elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _yo.color = _originalColor;
        yield return null;

    }
    public IEnumerator NextText()
    {
        if (_textIndex < 2)
        {
            yield return new WaitForSeconds(fadeTime * 2);
        }

        StartCoroutine(FadeOutText());
        yield return new WaitForSeconds(fadeTime);
        if (_textIndex < textos.Length)
        {
            _textIndex++;
        }
        _yo.text = textos[_textIndex];
        StartCoroutine(FadeInText());

        switch (_textIndex)
        {
            case 0: //bienvenido a ...
                yield return new WaitForSeconds(fadeTime);
                StartCoroutine(NextText());
                break;
            case 1: //el objetivo es...
                yield return new WaitForSeconds(fadeTime);
                StartCoroutine(NextText());
                break;
            case 2: //toca las flechas
                cuadrosRojos[0].gameObject.SetActive(true);
                break;
            case 3: //mantene el propulsor
                cuadrosRojos[0].gameObject.SetActive(false);
                cuadrosRojos[1].gameObject.SetActive(true);
                cuadrosRojos[4].gameObject.SetActive(true);

                break;
            case 4:  //una vez que cruzes la atmosfera
                cuadrosRojos[1].gameObject.SetActive(false);
                break;
            case 5: //esquiva los obstaculos y paredes
                break;
            case 6: //colecciona monedas
                break;
            case 7: //las cajas dan powerups
                break;
            case 8: //toca powerup para
                cuadrosRojos[2].gameObject.SetActive(true);
                break;
            case 9: //ojo que no se te acabe el gas
                cuadrosRojos[2].gameObject.SetActive(false);
                cuadrosRojos[3].gameObject.SetActive(true);
                yield return new WaitForSeconds(fadeTime * 5);
                StartCoroutine(NextText());
                break;
            case 10:
                break;
        }
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded) //cuando se destruye porque lo destrui a mano
        {
            //print("destrui a este shipthrusters on isloaded");
        }
        else //cuando se destruye porque cambie de escena
        {
            EventManager.Unsubscribe(Evento.BasePositionUp, BasePositionNext);
            EventManager.Unsubscribe(Evento.ThrusterDown, ThrusterNext);
            EventManager.Unsubscribe(Evento.NextTextWall, WallNext);
            EventManager.Unsubscribe(Evento.CajaPickup, CajaPickupNext);
            EventManager.Unsubscribe(Evento.PowerUpButtonUp, PowerUpNext);
        }
    }
}
