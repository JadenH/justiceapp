using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;
using _scripts;

[CustomEditor(typeof(Dragon))]
public class DragonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Dragon dragon = (Dragon)target;

        if (Application.isPlaying)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Default Game States", new GUIStyle
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 18,
                fontStyle = FontStyle.Bold
            });
            EditorGUILayout.Space();

            foreach (KeyValuePair<GameState, State> pair in
                new List<KeyValuePair<GameState, State>>(dragon.GameStates))
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel(pair.Key.ToString());
                var state = (State)EditorGUILayout.EnumPopup(pair.Value);
                if (state != pair.Value)
                {
                    dragon.SetGameState(pair.Key, state);
                }
                EditorGUILayout.EndHorizontal();
            }

            if (dragon.Model.Value != null)
            {
                EditorGUILayout.Space();
                if (GUILayout.Button("Copy State to Clipboard"))
                {
                    EditorGUIUtility.systemCopyBuffer = JsonConvert.SerializeObject(dragon.Model.Value);
                }
                EditorGUILayout.Space();
            }

        }
        else
        {
            DrawDefaultInspector();
        }
    }
}