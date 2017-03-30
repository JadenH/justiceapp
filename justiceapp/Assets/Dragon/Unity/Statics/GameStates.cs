using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using _scripts;

public static class GameStates
{
    public static void SetAll(State state, bool skipAnimation = false)
    {
        foreach (GameState gameState in Enum.GetValues(typeof(GameState)))
        {
            if (gameState < 0) continue;
            gameState.Set(state, skipAnimation);
        }
    }

    public static bool Is(this GameState gameState, State state)
    {
        return Dragon.Instance.GameStates[gameState] == state;
    }

    public static void Set(this GameState gameState, State state, bool skipAnimation = false)
    {
        Dragon.Instance.SetGameState(gameState, state, skipAnimation);
    }

    public static void Set(this GameState gameState, State state, float delay, bool skipAnimation = false)
    {
        Dragon.Instance.StartCoroutine(SetDelay(gameState, state, delay, skipAnimation));
    }

    private static IEnumerator SetDelay(this GameState gameState, State state, float delay, bool skipAnimation = false)
    {
        yield return new WaitForSeconds(delay);
        Dragon.Instance.SetGameState(gameState, state, skipAnimation);
    }

    public static void Set(this IEnumerable<GameState> gameStates, State state, bool skipAnimation = false)
    {
        foreach (var gameState in gameStates)
        {
            gameState.Set(state, skipAnimation);
        }
    }

    public static bool AllAre(this IEnumerable<GameState> gameStates, State state)
    {
        return gameStates.All(gameState => gameState.Is(state));
    }

    public static bool AnyAre(this IEnumerable<GameState> gameStates, State state)
    {
        return gameStates.Any(gameState => gameState.Is(state));
    }
}