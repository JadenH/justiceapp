using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public partial class DragonStore<T> : IDragonStore
{
    [JsonIgnore]
    public Action OnDestroyed { get; set; }

    [JsonIgnore]
    public Dragon Dragon
    {
        get { return Dragon.Instance; }
    }

    [JsonIgnore]
    public Dragon.DragonDispachter Dispatcher
    {
        get { return Dragon.Instance.Dispachter; }
    }


    public T Value
    {
        get { return _value; }
        set
        {
            if (!Equals(value, _value))
            {
                _prevValue = _value;
                _value = value;
                Notify();
            }
        }
    }

    public T _prevValue;
    private T _value;

    public DragonStore(T value)
    {
        Bindings = new List<IDragonBehaviour>();
        Value = value;
        InitEvents();
        Dragon.RegisterStore(this);
    }

    private readonly List<Action<T, T>> _subscribers = new List<Action<T, T>>();

    public void OnChange(IDragonSubscriber subscriber, Action<T, T> action)
    {
        // Immediately get the gameobject in sync with state.
        _subscribers.Add(action);
        if (subscriber.IsActive)
        {
            action.Invoke(_value, _prevValue);
        }

        // Sync up state when gameobject enables & add to subscribers.
        subscriber.OnEnabled += () =>
        {
            action.Invoke(_value, _prevValue);
            _subscribers.Add(action);
        };

        subscriber.OnDisabled += () =>
        {
            if (_subscribers.Contains(action))
            {
                _subscribers.Remove(action);
            }
        };

        subscriber.OnUnsubscribed += () =>
        {
            if (_subscribers.Contains(action))
            {
                _subscribers.Remove(action);
            }
        };
    }

    public void Notify()
    {
        foreach (var subscriber in _subscribers)
        {
            subscriber.Invoke(_value, _prevValue);
        }
    }

    public void Prioritize()
    {
        Dragon.SetPriority(this);
    }

    public void Deprioritize()
    {
        if (Dragon.GetPriority<IDragonStore>(GetType()) == this)
        {
            Dragon.ClearPriority(GetType());
        }
    }
}