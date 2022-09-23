using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinWall : TriggerCollider
{
    //esta wall te hace ganar y avisa al levelmanager que nivel completaste
    public int numeroDeEsteNivel;
    string _esteNivel;

    private void Start()
    {
        _esteNivel = SceneManager.GetActiveScene().name;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        //base.OnTriggerEnter(other);
        EventManager.Trigger(Evento.WinWall, _esteNivel, numeroDeEsteNivel);
    }
}
