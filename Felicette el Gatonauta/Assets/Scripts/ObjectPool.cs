using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ObjectPool<T>
{
    public delegate T FactoryMethod();

    List<T> _currentStock = new List<T>();
    FactoryMethod _factoryMethod;
    Action<T> _turnOnCallback;
    Action<T> _turnOffCallback;

    public ObjectPool(FactoryMethod factory, Action<T> turnOn, Action<T> turnOff, int initialStock = 5)
    {
        _factoryMethod = factory;
        _turnOnCallback = turnOn;
        _turnOffCallback = turnOff;

        for (int i = 0; i < initialStock; i++) //cuando nace, objectpool crea todas
        {
            //Debug.Log("construi la bala " + i);
            var o = _factoryMethod();
            _turnOffCallback(o);
            _currentStock.Add(o);
        }
    }

    public T GetObject()
    {
        T result;

        if (_currentStock.Count > 0)
        {
            result = _currentStock[0];
            _currentStock.RemoveAt(0);
        }
        else
        {
            result = _factoryMethod();
        }

        _turnOnCallback(result);
        //Debug.Log("GetObject: te estoy dando el objeto " + result);
        //Debug.Log("hay " + _currentStock.Count + " objetos en la pool");
        return result;
    }

    public void ReturnObject(T o)
    {
        _turnOffCallback(o);
        _currentStock.Add(o);
        //Debug.Log("ReturnObject: devolvi el objeto " + o);
        //Debug.Log("hay " + _currentStock.Count + " objetos en la pool");
    }

}
