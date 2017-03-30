using System;
using System.Collections;
using System.Collections.Generic;

public class DragonList<T> : ICollection<T> where T : IDragonStore
{
    private readonly List<Action<T>> _onAddSubscribers = new List<Action<T>>();
    private readonly List<Action<T>> _onRemoveSubscribers = new List<Action<T>>();

    private List<T> _value { get; set; }

    public DragonList()
    {
        _value = new List<T>();
    }

    public void Add(T item)
    {
        _value.Add(item);
        foreach (var subscriber in _onAddSubscribers)
        {
            subscriber.Invoke(item);
        }
        item.OnDestroyed += () => Remove(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    bool ICollection<T>.Remove(T item)
    {
        var result = _value.Remove(item);
        if (result)
        {
            foreach (var subscriber in _onRemoveSubscribers)
            {
                subscriber.Invoke(item);
            }
        }
        return result;
    }

    public bool Remove(T item)
    {
        var result = _value.Remove(item);
        if (result)
        {
            foreach (var subscriber in _onRemoveSubscribers)
            {
                subscriber.Invoke(item);
            }
        }
        return result;
    }

    public int Count { get; private set; }
    public bool IsReadOnly { get; private set; }

    private void NotifyExisting(Action<T> action)
    {
        // Invoke for data that already exists.
        foreach (var data in _value)
        {
            action.Invoke(data);
        }
    }

    public void OnAddItem(IDragonBehaviour behaviour, Action<T> action)
    {
        if (behaviour != null)
        {
            if (behaviour.IsActive)
            {
                NotifyExisting(action);
                _onAddSubscribers.Add(action);
            }

            behaviour.OnEnabled += () =>
            {
                NotifyExisting(action);
                _onAddSubscribers.Add(action);
            };

            behaviour.OnDisabled += () =>
            {
                _onAddSubscribers.Remove(action);
            };
        }
        else
        {
            throw new NullReferenceException("dragonObject can't be null.");
        }

    }

    public void OnRemoveItem(IDragonBehaviour dragonBehaviourObject, Action<T> action)
    {
        if (dragonBehaviourObject != null)
        {
            _onRemoveSubscribers.Add(action);

            dragonBehaviourObject.OnEnabled += () =>
            {
                _onRemoveSubscribers.Add(action);
            };

            dragonBehaviourObject.OnDisabled += () =>
            {
                _onRemoveSubscribers.Remove(action);
            };
        }
        else
        {
            throw new NullReferenceException("dragonObject can't be null.");
        }
    }

    public void RemoveAt(int i)
    {
        T item = _value[i];
        if (item != null)
        {
            _value.RemoveAt(i);
            foreach (var subscriber in _onRemoveSubscribers)
            {
                subscriber.Invoke(item);
            }
        }

    }

    public void Clear()
    {
        foreach (var subscriber in _onRemoveSubscribers)
        {
            foreach (var item in _value)
            {
                subscriber.Invoke(item);
            }
        }
        _value.Clear();
    }

    public bool Contains(T item)
    {
        return _value.Contains(item);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _value.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}