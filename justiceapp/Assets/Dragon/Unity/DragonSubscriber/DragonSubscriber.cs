using System;
using UnityEngine;

public abstract partial class DragonSubscriber<T> : DragonBehaviour, IDragonSubscriber where T : IDragonStore
{
    public Action OnUnsubscribed { get; set; }

    protected override void Awake()
    {
        InitSubscribers();
        base.Awake();
    }


}