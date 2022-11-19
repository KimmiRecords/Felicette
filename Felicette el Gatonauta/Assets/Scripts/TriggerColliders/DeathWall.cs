using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathWall : TriggerCollider
{
    string _esteNivel;

    private void Start()
    {
        _esteNivel = SceneManager.GetActiveScene().name;
    }

    public override void Activate()
    {
        base.Activate();
        AudioManager.instance.PlayByName("ShipCrash");
        EventManager.Trigger(Evento.DeathWall, _esteNivel);
    }
}
