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

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        EventManager.Trigger(Evento.DeathWall, _esteNivel);
    }
}
