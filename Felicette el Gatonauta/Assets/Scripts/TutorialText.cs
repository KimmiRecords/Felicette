using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMPro.TextMeshProUGUI))]
public class TutorialText : MonoBehaviour
{
    TMPro.TextMeshProUGUI yo;
    Color originalColor;
    int textIndex;

    public float fadeTime;
    public string[] textos;
    public Image[] cuadrosRojos;

    
    void Start()
    {
        yo = GetComponent<TMPro.TextMeshProUGUI>();
        originalColor = yo.color;
        textIndex = 0;
        yo.text = textos[textIndex];

        EventManager.Subscribe(Evento.BasePositionUp, BasePositionTuki);
        EventManager.Subscribe(Evento.ThrusterDown, ThrusterTuki);
        EventManager.Subscribe(Evento.NextTextWall, WallTuki);
        EventManager.Subscribe(Evento.CajaPickup, CajaPickupTuki);
        EventManager.Subscribe(Evento.PowerUpButtonUp, PowerUpTuki);

        StartCoroutine(NextText());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(NextText());
        }
    }

    //estos metodos son para prevenir que se sobre disparen los textos
    public void BasePositionTuki(params object[] parameters)
    {
        if (textIndex == 2)
        {
            StartCoroutine(NextText());
        }
        else
        {
            return;
        }
    }
    public void ThrusterTuki(params object[] parameters)
    {
        if (textIndex == 3)
        {
            StartCoroutine(NextText());
        }
        else
        {
            return;
        }
    }
    public void WallTuki(params object[] parameters)
    {
        if (parameters[0] is int)
        {
            if (textIndex == (int)parameters[0])
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
    public void CajaPickupTuki(params object[] parameters)
    {
        if (textIndex == 7)
        {
            StartCoroutine(NextText());
        }
        else
        {
            return;
        }
    }
    public void PowerUpTuki(params object[] parameters)
    {
        if (textIndex == 8)
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
            yo.color = Color.Lerp(yo.color, Color.clear, elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yo.color = Color.clear;
        yield return null;

        //StartCoroutine(FadeInText());
    }

    public IEnumerator FadeInText()
    {
        float elapsedTime = 0;

        while (elapsedTime < fadeTime)
        {
            yo.color = Color.Lerp(Color.clear, originalColor, elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yo.color = originalColor;
        yield return null;

    }

    public IEnumerator NextText()
    {
        if (textIndex < 2)
        {
            yield return new WaitForSeconds(fadeTime * 2);
        }

        StartCoroutine(FadeOutText());
        yield return new WaitForSeconds(fadeTime);
        if (textIndex < textos.Length)
        {
            textIndex++;
        }
        yo.text = textos[textIndex];
        StartCoroutine(FadeInText());

        switch (textIndex)
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
                yield return new WaitForSeconds(fadeTime);
                textIndex++;
                break;
            case 10:
                break;
        }
    }

    //on destroy
    //bal bla bla
    //seguro se rompe todo x lo del subscribe

}
