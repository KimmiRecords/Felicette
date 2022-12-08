using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAoE : TriggerCollider
{
    public float planetMass;
    

    [Header("Gravity Area of Effect")]
    public SpriteRenderer aoeSpriteRenderer;
    protected Color aoeSpriteOriginalColor;

    public LineRenderer lineToPlayer;
    protected Vector3[] allLinePositions = new Vector3[2];

    


    private void Start()
    {
        aoeSpriteOriginalColor = aoeSpriteRenderer.color;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IGravity>() != null)
        {
            AudioManager.instance.PlayByName("GravityAoE");
        }
    }

    protected override void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<IGravity>() != null)
        {
            var afectado = other.GetComponent<IGravity>();
            afectado.ApplyGravity(transform.position, planetMass);

            //print("gravityAoE: stay");
            if (aoeSpriteRenderer != null)
            {
                aoeSpriteRenderer.color += new Color(0, 0, 0, 0.1f);
            }

            allLinePositions[0] = transform.position;
            allLinePositions[1] = other.gameObject.transform.position;
            lineToPlayer.SetPositions(allLinePositions);
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IGravity>() != null)
        {
            AudioManager.instance.StopByName("GravityAoE");
            if (aoeSpriteRenderer != null)
            {
                aoeSpriteRenderer.color = aoeSpriteOriginalColor;
            }
        }
    }

}
