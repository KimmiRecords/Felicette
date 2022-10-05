using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityForce
{
    //una clase con solo variables y metodos staticos. otras clases aprovechan esto.
    //sirve para pedirle el calculo de la fuerza de gravedad, en forma de vector3 o float.

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
