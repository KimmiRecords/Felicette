using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PowerUpButton : BaseButton
{
    //este es el boton de la ui que cuando lo tocas activas el powerup

    Button _yo;
    Image _image;
    Sprite _defaultSprite;

    TMPro.TextMeshProUGUI _miTexto;
    Color _originalColor;

    public override void OnPointerUp(PointerEventData eventData)
    {
        EventManager.Trigger(Evento.PowerUpButtonUp);
        ResetColor();
    }

    private void Start()
    {

        _yo = GetComponent<Button>();
        _image = GetComponent<Image>();
        _defaultSprite = _image.sprite;

        if (GetComponentInChildren<TMPro.TextMeshProUGUI>() != null) //todos los botones tienen un hijo textmeshpro, pero por las dudas chequeo
        {
            _miTexto = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        }
        
        _originalColor = _yo.colors.normalColor;
        EventManager.Subscribe(Evento.GotPowerUp, ChangePowerUpSprite);
    }

    void ChangePowerUpSprite(params object[] parameters)
    {
        //pide por parametro qué powerup se consiguió para actualizar el color y texto del boton de powerup
        //me pasan por parametro el texto y el sprite que va


        //ColorBlock cb = _yo.colors;
        //cb.normalColor = Color.green;
        //_yo.colors = cb;

        //if (parameters[0] is int)
        //{
        //    switch ((int)parameters[0])
        //    {
        //        case 1: //si en la caja sale 0, es gas powerup
        //            _miTexto.text = "GAS REFILL";
        //            _sr.sprite = 
        //            break;
        //        case 2: //si sale 1, es speed powerup
        //            _miTexto.text = "MODO CHIQUITO";
        //            break;
        //        case 3: //2 es randomcoins
        //            _miTexto.text = "LLUVIA DE MONEDAS";
        //            break;
        //    }
        //}
        //else
        //{
        //    print("ojo, no me pasaste int de primer parametro");
        //}

        _miTexto.text = (string)parameters[1];
        _image.sprite = (Sprite)parameters[2];

    }

    private void ResetColor(params object[] parameters)
    {
        ColorBlock cb = _yo.colors;
        cb.normalColor = _originalColor;
        _yo.colors = cb;
        _miTexto.text = "";
        _image.sprite = _defaultSprite;

    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded) 
        {
            //print("destrui a este shipthrusters on isloaded");
        }
        else 
        {
            EventManager.Unsubscribe(Evento.GotPowerUp, ChangePowerUpSprite);
            //print("destrui a este shipthrusters on sceneclosure");
        }
    }
}
