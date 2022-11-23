using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory<T> where T : MonoBehaviour
{
    //la factory generica 

    [SerializeField]
    protected T prefab;

    //public Factory(T objPrefab)
    //{
    //    prefab = objPrefab;
    //}

    public T Get()
    {
        return GameObject.Instantiate(prefab);
    }

}
