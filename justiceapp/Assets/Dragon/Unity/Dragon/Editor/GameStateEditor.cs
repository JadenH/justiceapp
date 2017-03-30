using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using _scripts;

[CustomEditor(typeof(DefaultGameState))]
public class GameStateEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DefaultGameState defaultGameState = (DefaultGameState)target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Default Game States", new GUIStyle
        {
            alignment = TextAnchor.MiddleCenter,
            fontSize = 18,
            fontStyle = FontStyle.Bold
        });
        EditorGUILayout.Space();

        foreach (GameState gameState in Enum.GetValues(typeof(GameState)))
        {
            if (!defaultGameState.GameStateInfos.Exists(info => info.GameState == gameState))
            {
                defaultGameState.GameStateInfos.Add(new DefaultGameState.GameStateInfo
                {
                    GameState = gameState,
                    State = State.Disabled
                });
            }
        }

        foreach (DefaultGameState.GameStateInfo info in defaultGameState.GameStateInfos)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(info.GameState.ToString());
            info.State = (State)EditorGUILayout.EnumPopup(info.State);
            EditorGUILayout.EndHorizontal();
        }
    }
}