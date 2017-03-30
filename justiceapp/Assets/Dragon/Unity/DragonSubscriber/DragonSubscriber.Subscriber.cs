using System;

public abstract partial class DragonSubscriber<T>
{
    private T _subscribedData;
    private T _data;

    public T Data
    {
        get { return _data; }
        set
        {
            if (_data != null)
            {
                OnUnsubscribe();
                if (OnUnsubscribed != null) OnUnsubscribed.Invoke();
            }
            _data = value;
            if (value != null) OnSubscribe(value);
        }
    }

    public IDragonStore DragonStore { get { return Data; } }

    protected abstract void OnSubscribe(T dragonObject);
    protected abstract void OnUnsubscribe();

    private void InitSubscribers()
    {
        OnDisabled += () =>
        {
            OnUnsubscribe();
            if (OnUnsubscribed != null) OnUnsubscribed.Invoke();
        };

        OnDestroyed += UnBind;
    }

    public void SubscribeTo(T dragonObject, bool bind = false)
    {
        _subscribedData = dragonObject;
        Data = _subscribedData;
        if (bind) BindWith(_subscribedData);
    }

    protected void BindWith(IDragonStore store)
    {
        store.BindWith(this);
    }

    protected void UnBind()
    {
        if (_data != null) _data.UnbindWith(this);
    }
}