using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public delegate void EventReceiver(params object[] parameters);

    //en vez de string, la posta seria usar enum, cosa de no poder equivocarme
    static Dictionary<string, EventReceiver> _events = new Dictionary<string, EventReceiver>();

    
    public static void Subscribe(string eventName, EventReceiver listener)
    {
        if (!_events.ContainsKey(eventName))
        {
            _events.Add(eventName, listener);
        }
        else
        {
            _events[eventName] += listener;
        }
    }

    public static void Unsubscribe(string eventName, EventReceiver listener)
    {
        if (_events.ContainsKey(eventName))
        {
            _events[eventName] -= listener;
        }
    }

    public static void Trigger(string eventName, params object[] parameters)
    {
        if (_events.ContainsKey(eventName))
        {
            _events[eventName](parameters);
        }
    }
}
