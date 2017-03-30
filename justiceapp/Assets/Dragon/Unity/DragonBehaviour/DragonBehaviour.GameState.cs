using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using _scripts;

public abstract partial class DragonBehaviour
{
    private List<GameState> _gameStates;

    private void InitGameState()
    {
        _gameStates = GetType().GetCustomAttributes(typeof(GameStateAttribute), true)
            .Cast<GameStateAttribute>().Select(attribute => attribute.GameState).ToList();

        SubscribeGameStates();
    }

    private void SubscribeGameStates()
    {
        foreach (GameState gameState in new List<GameState>(_gameStates))
        {
            SubscribeTo(gameState);
        }
    }

    public void SubscribeTo(GameState gameState)
    {
        _gameStates.Add(gameState);
        Dragon.Subscribe(this, gameState, Time.frameCount <= 1);
    }

    public void UnSubscribe(GameState gameState)
    {
        if (_gameStates.Contains(gameState))
        {
            _gameStates.Remove(gameState);
            Dragon.UnSubscribe(this, gameState);
        }
    }

    public bool GameStatesEnabled()
    {
        return !_gameStates.Any() || _gameStates.All(g => g.Is(State.Enabled));
    }

    public virtual void OnGameStateChanged(GameState gameState, State state, bool skipAnimation = false)
    {
        var objectState = GameStatesEnabled();

        var animator = this as IDragonAnimator;
        if (animator != null)
        {
            GameStateAnimation(objectState, animator, skipAnimation);
        }
        else
        {
            SetCanvasInteractable(objectState);
            gameObject.SetActive(objectState);
        }
    }

    public virtual bool ShouldActivate(Selectable selectable)
    {
        return true;
    }


    private void SetCanvasInteractable(bool state)
    {
        foreach (var child in GetComponentsInChildren<Selectable>())
        {
            if (child.IsDestroyed()) continue;
            child.interactable = state && ShouldActivate(child);
        }
    }
}