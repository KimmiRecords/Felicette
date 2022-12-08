using System.Collections;
using UnityEngine;

public class ShipView
{
    //el view del mvc
    Animator _anim;
    SpriteRenderer _naveSr;
    ShipThrusters _shipThrusters;

    public ShipView(ShipThrusters st)
    {
        //Debug.Log("view created");
        _shipThrusters = st;

        _anim = _shipThrusters.anim;
        _naveSr = _shipThrusters.naveSpriteRenderer;

        EventManager.Subscribe(Evento.ModoChiquitoStart, StartRescale);
        EventManager.Subscribe(Evento.CoinRainStart, CoinRainAnimationStart);
        EventManager.Subscribe(Evento.ThrusterDown, StartThrusterFX);
        EventManager.Subscribe(Evento.ThrusterUp, EndThrusterFX);
        EventManager.Subscribe(Evento.StartDeathSequence, StartDeathSequence);
        EventManager.Subscribe(Evento.GetShield, GetShieldFX);


        _naveSr.sprite = SkinsManager.instance.currentSkin;
    }

    public void CoinRainAnimationStart(params object[] parameters)
    {
        _anim.SetTrigger("Monedas");

    }
    public void StartRescale(params object[] parameters)
    {
        _shipThrusters.StartCoroutine(Rescale());
    }

    public IEnumerator Rescale()
    {
        Vector3 originalScale = _shipThrusters.transform.localScale;

        //pasan cosas
        //print("arranca el boost");
        _shipThrusters.transform.localScale = Vector3.one * _shipThrusters.rescaleFactor;

        yield return new WaitForSeconds(1);
        AudioManager.instance.PlayByName("TimerFourTicks");

        yield return new WaitForSeconds(_shipThrusters.rescaleDuration - 1);

        //terminan de pasar cosas
        //print("termina el boost");
        AudioManager.instance.PlayByName("ModoChiquitoOff");
        EventManager.Trigger(Evento.ModoChiquitoEnd);
        _shipThrusters.transform.localScale = originalScale;

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
        if (_shipThrusters.canThrust)
        {
            AudioManager.instance.PlayByName("PropulsoresSFX");
            _anim.SetBool("IsThrusting", true);
            _shipThrusters.fuegoGameObject.SetActive(true);
        }
    }

    public void EndThrusterFX(params object[] parameters)
    {
        AudioManager.instance.StopByName("PropulsoresSFX");
        _anim.SetBool("IsThrusting", false);
        _shipThrusters.fuegoGameObject.SetActive(false);

    }

    public void StartDeathSequence(params object[] parameters)
    {
        _anim.SetTrigger("Choque");

        //Debug.Log("ship View: start death sequence");
        Vector3 currentPos = _shipThrusters.transform.position;
        //Debug.Log("set current pos");

        _shipThrusters.transform.position = currentPos;
        //Debug.Log("set transform to current pos");

        _shipThrusters.canThrust = false;
        //Debug.Log("can thrust = false");

        _shipThrusters.myRigidBody.constraints = RigidbodyConstraints.FreezeAll;
        //Debug.Log("rb freeze");
    }

    public void GetShieldFX(params object[] parameters)
    {
        Debug.Log("getshieldfx");
        AudioManager.instance.PlayByNamePitch("Zap1", 0.5f);
        _shipThrusters.shieldGameObject.SetActive(true);
    }

    public void LoseShieldFX(params object[] parameters)
    {
        Debug.Log("loseshieldfx");
        AudioManager.instance.PlayByNamePitch("Zap1", 0.2f);
        _shipThrusters.shieldGameObject.SetActive(false);
    }
}
