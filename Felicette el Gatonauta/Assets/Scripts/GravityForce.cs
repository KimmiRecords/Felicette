using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityForce
{
    public static float gravityConstant = 1000;

    public static Vector3 GetGravityVector3(float masa1, float masa2, float distancia, Vector3 direccionHaciaElPlaneta)
    {
        Vector3 totalGravityForce;
        totalGravityForce = (direccionHaciaElPlaneta.normalized) * (gravityConstant * (masa1 * masa2) / (distancia * distancia));
        return totalGravityForce;
    }

    public static float GetGravityFloat(float masa1, float masa2, float distancia)
    {
        float totalGravityForce;
        totalGravityForce = gravityConstant * (masa1 * masa2) / (distancia * distancia);
        return totalGravityForce;
    }
}
