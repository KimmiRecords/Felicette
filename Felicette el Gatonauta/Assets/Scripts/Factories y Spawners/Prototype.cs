using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Prototype : MonoBehaviour
{
    //clase abstracta base, de la cual van a heredar
    public abstract Prototype Clone();
}