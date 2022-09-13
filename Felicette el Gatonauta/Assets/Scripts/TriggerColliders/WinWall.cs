using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinWall : TriggerCollider
{
    string esteNivel;

    private void Start()
    {
        esteNivel = SceneManager.GetActiveScene().name;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        //base.OnTriggerEnter(other);
        EventManager.Trigger(Evento.WinWall, esteNivel);
    }
}
