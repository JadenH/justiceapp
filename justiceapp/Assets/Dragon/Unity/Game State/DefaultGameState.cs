using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using _scripts;

[Serializable]
public class DefaultGameState : ScriptableObject
{

    private Dictionary<GameState, State> _gameStates;
    public Dictionary<GameState, State> GameStates
    {
        get
        {
            if (_gameStates != null)
            {
                return _gameStates;
            }
            _gameStates = GameStateInfos.ToDictionary(info => info.GameState, info => info.State);
            foreach (GameState gameState in Enum.GetValues(typeof(GameState)))
            {
                if (!_gameStates.ContainsKey(gameState)) _gameStates.Add(gameState, State.Disabled);
            }
            return _gameStates;
        }
    }

    public List<GameStateInfo> GameStateInfos = new List<GameStateInfo>();

    [Serializable]
    public class GameStateInfo
    {
        public GameState GameState;
        public State State;
    }
}