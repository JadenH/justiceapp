using System;
using System.Collections.Generic;
using UnityEngine;
using _scripts;

public partial class Dragon
{
    public DefaultGameState DefaultGameStates;

    public Dictionary<GameState, State> GameStates
    {
        get { return DefaultGameStates.GameStates; }
    }

    public Dictionary<GameState, List<IDragonBehaviour>> GameStateSubscribers = new Dictionary<GameState, List<IDragonBehaviour>>();

    protected void InitGameState()
    {

    }

    public void SetGameState(GameState gameState, State state, bool skipAnimation = false)
    {
        if (GameStates[gameState] != state)
        {
            GameStates[gameState] = state;
            NotifyGameStateSubscribers(gameState, state, skipAnimation);
        }
    }

    private void NotifyGameStateSubscribers(GameState gameState, State state, bool skipAnimation = false)
    {
        if (GameStateSubscribers.ContainsKey(gameState))
        {
            GameStateSubscribers[gameState].ForEach(
                dragonBehaviour => dragonBehaviour.OnGameStateChanged(gameState, state, skipAnimation));
        }
        else
        {
            Debug.LogWarning("No gamestate subscribers to notify for '" + gameState + "'", gameObject);
        }
    }

    public void Subscribe(IDragonBehaviour obj, GameState gameState, bool skipAnimation = false)
    {
        if (!GameStateSubscribers.ContainsKey(gameState)) GameStateSubscribers[gameState] = new List<IDragonBehaviour>();

        obj.OnDestroyed += () => GameStateSubscribers[gameState].Remove(obj);
        GameStateSubscribers[gameState].Add(obj);

        if (!GameStates.ContainsKey(gameState)) GameStates[gameState] = State.Disabled;
        obj.OnGameStateChanged(gameState, GameStates[gameState], skipAnimation);
    }

    public void UnSubscribe(IDragonBehaviour obj, GameState gameState)
    {
        GameStateSubscribers[gameState].Remove(obj);
    }
}

public enum State
{
    Enabled,
    Disabled
}