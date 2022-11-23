using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePowerUp : IPowerUp
{
    //este powerup te cambia el tamaño de la nave durante unos segundos

    public void Activate(Ship s)
    {
        AudioManager.instance.PlayByName("ModoChiquitoOn");

        //Debug.Log("cambio la scale");
        EventManager.Trigger(Evento.ModoChiquitoStart);
    }
}
