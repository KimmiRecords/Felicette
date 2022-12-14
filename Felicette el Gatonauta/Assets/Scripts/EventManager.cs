using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Evento
{
    //triggers
    WinWall,
    DeathWall,
    StartDeathSequence,
    AtmosphereWall,
    CoinPickup,
    CajaPickup,
    NextTextWall,

    //botones
    BasePositionDown,
    BasePositionUp,
    ThrusterDown,
    ThrusterUp,
    QuitGameButtonUp,
    ExitLevelButtonUp,
    GoToSceneButtonUp,
    ResetLevelButtonUp,
    EraseDataButtonUp,
    PowerUpButtonUp,
    PauseButtonUp,
    UnpauseButtonUp,
    EquipItemButtonUp,
    UnequipItem,
    WatchAdButtonUp,
    WatchAdButtonDown,
    SplashButtonUp,
    ConfirmButtonUp,
    CancelButtonUp,
    GachaButtonUp,

    //otros
    BurnGas,
    RefillGas,
    CoinUpdate,
    StaminaUpdate,
    GotPowerUp,
    ModoChiquitoStart,
    ModoChiquitoEnd,
    CoinRainStart,
    GetShield
    
}
public class EventManager
{
    public delegate void EventReceiver(params object[] parameters);

    static Dictionary<Evento, EventReceiver> _events = new Dictionary<Evento, EventReceiver>();

    
    public static void Subscribe(Evento evento, EventReceiver metodo)
    {
        if (!_events.ContainsKey(evento))
        {
            _events.Add(evento, metodo);
        }
        else
        {
            _events[evento] += metodo;
        }
    }

    public static void Unsubscribe(Evento evento, EventReceiver metodo)
    {
        if (_events.ContainsKey(evento))
        {
            _events[evento] -= metodo;
        }
    }

    public static void Trigger(Evento evento, params object[] parameters)
    {
        if (_events.ContainsKey(evento))
        {
            _events[evento](parameters);
        }
    }
}
