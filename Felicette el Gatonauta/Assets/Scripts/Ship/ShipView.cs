using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipView
{
    //el view del mvc
    Animator _anim;
    ShipModel _model;
    SpriteRenderer _itemSr;

    public ShipView(ShipModel sm, Animator a, SpriteRenderer sr)
    {
        //Debug.Log("view created");
        _model = sm;
        _anim = a;
        _itemSr = sr;

        EventManager.Subscribe(Evento.ModoChiquitoStart, StartRescale);
        EventManager.Subscribe(Evento.CoinRainStart, CoinRainAnimationStart);
        EventManager.Subscribe(Evento.ThrusterDown, StartThrusterFX);
        EventManager.Subscribe(Evento.ThrusterUp, EndThrusterFX);

        _itemSr.sprite = SkinsManager.instance.currentSkin;
    }

    public void CoinRainAnimationStart(params object[] parameters)
    {
        _anim.SetTrigger("Monedas");

    }
    public void StartRescale(params object[] parameters)
    {
        _model.ship.StartCoroutine(Rescale());
    }

    public IEnumerator Rescale()
    {
        Vector3 originalScale = _model.ship.transform.localScale;

        //pasan cosas
        //print("arranca el boost");
        _model.ship.transform.localScale = Vector3.one * _model.ship.rescaleFactor;

        yield return new WaitForSeconds(1);
        AudioManager.instance.PlayByName("TimerFourTicks");

        yield return new WaitForSeconds(_model.ship.rescaleDuration - 1);

        //terminan de pasar cosas
        //print("termina el boost");
        AudioManager.instance.PlayByName("ModoChiquitoOff");
        EventManager.Trigger(Evento.ModoChiquitoEnd);
        _model.ship.transform.localScale = originalScale;

    }

    public void EscapeAtmosphereFX(params object[] parameters)
    {
        AudioManager.instance.StopByName("RadioPreLaunchSFX");

        if (!AudioManager.instance.sound["EroicaLoop"].isPlaying)
        {
            AudioManager.instance.PlayByName("EroicaLoop");
        }
    }

    public void ApplyGravityFX()
    {
        //AudioManager.instance.PlayByName("GravityAoE"); //esto es mejor si lo hace el planeta con su ontrigger enter stay exit
        //efectitos de sprite de la ship si los hubiera
    }

    public void StartThrusterFX(params object[] parameters)
    {
        if (_model.ship.canThrust)
        {
            AudioManager.instance.PlayByName("PropulsoresSFX");
            _anim.SetBool("IsThrusting", true);
        }
    }

    public void EndThrusterFX(params object[] parameters)
    {
        AudioManager.instance.StopByName("PropulsoresSFX");
        _anim.SetBool("IsThrusting", false);
    }

    //public void EquipItem(params object[] parameters)
    //{
    //    Debug.Log("ship view: equipe el item " + (Sprite)parameters[0]);
    //    _itemSr.sprite = (Sprite)parameters[0];
    //}
    //public void UnequipItem(params object[] parameters)
    //{
    //    Debug.Log("ship view: desequipe todo");
    //    _itemSr.sprite = null;
    //}
}
