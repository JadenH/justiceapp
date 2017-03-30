using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public abstract partial class DragonBehaviour : MonoBehaviour, IDragonBehaviour
{
    public Action OnEnabled { get; set; }
    public Action OnDisabled { get; set; }
    public Action OnDestroyed { get; set; }


    public Dragon Dragon
    {
        get { return Dragon.Instance; }
    }

    public Dragon.DragonDispachter Dispatcher
    {
        get { return Dragon.Instance.Dispachter; }
    }

    protected virtual void Awake()
    {
        InitGameState();
        InitAnimation();
    }

    public bool IsActive
    {
        get { return isActiveAndEnabled; }
    }

    protected virtual void OnEnable()
    {
        if (OnEnabled != null) OnEnabled.Invoke();
    }

    protected virtual void OnDisable()
    {
        if (OnDisabled != null) OnDisabled.Invoke();
    }

    protected virtual void OnDestroy()
    {
        if (OnDestroyed != null) OnDestroyed.Invoke();
    }

    public void DoDestroy()
    {
        var animator = this as IDragonAnimator;
        if (animator != null)
        {
            AnimateOut(animator, true);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}