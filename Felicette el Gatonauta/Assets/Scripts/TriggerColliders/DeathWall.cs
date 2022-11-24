using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathWall : TriggerCollider
{
    string _esteNivel;
    float timeToSceneChange = 1.5f;


    private void Start()
    {
        _esteNivel = SceneManager.GetActiveScene().name;
    }

    public override void Activate()
    {
        if (!LevelManager.instance.inDeathSequence)
        {
            StartCoroutine(StartDeathSequence());
        }
    }

    public IEnumerator StartDeathSequence()
    {
        LevelManager.instance.inDeathSequence = true;
        AudioManager.instance.PlayByName("ShipCrash");
        AudioManager.instance.PlayByName("Explosion");

        EventManager.Trigger(Evento.StartDeathSequence);

        yield return new WaitForSeconds(timeToSceneChange);
        EventManager.Trigger(Evento.DeathWall, _esteNivel);
        LevelManager.instance.inDeathSequence = true;



    }
}
