using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinWall : TriggerCollider
{
    //esta wall te hace ganar y avisa al levelmanager cual nivel completaste

    public int numeroDeEsteNivel;
    //tuto es 0, nivel 1 es 1, nivel 2 es 2...

    string _esteNivel;

    private void Start()
    {
        _esteNivel = SceneManager.GetActiveScene().name;
    }

    public override void Activate()
    {
        base.Activate();
        EventManager.Trigger(Evento.WinWall, _esteNivel, numeroDeEsteNivel);

    }
}
