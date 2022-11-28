using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathWall : TriggerCollider
{
    string _esteNivel;
    float timeToSceneChange = 1f;


    private void Start()
    {
        _esteNivel = SceneManager.GetActiveScene().name;
    }

    public override void Activate()
    {
        print("activo deathwall");
        if (!LevelManager.instance.inDeathSequence)
        {
            print("start death sequence");

            StartCoroutine(StartDeathSequence());
        }
    }

    public IEnumerator StartDeathSequence()
    {
        LevelManager.instance.inDeathSequence = true;
        print("indeath sequence = true");
        AudioManager.instance.PlayByName("ShipCrash");
        AudioManager.instance.PlayByName("Explosion");
        print("played sounds");


        EventManager.Trigger(Evento.StartDeathSequence);
        print("event trigger: startdeathsequence");


        yield return new WaitForSeconds(timeToSceneChange);
        print("espere segundos");


        EventManager.Trigger(Evento.DeathWall, _esteNivel);
        print("event trigger: deathwall");

        LevelManager.instance.inDeathSequence = false;
        print("indeath sequence = false");




    }
}
