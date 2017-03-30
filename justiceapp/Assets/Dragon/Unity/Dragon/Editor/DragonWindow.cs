using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using _scripts;

public class DragonWindow : EditorWindow
{
    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/Game States")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        DragonWindow window = (DragonWindow)GetWindow(typeof(DragonWindow));
        window.Show();
    }

    private static Dragon Dragon
    {
        get { return Dragon.Instance; }
    }

    private void OnGUI()
    {
        GUILayout.Label("Game States", EditorStyles.boldLabel);

        if (Dragon)
        {
            foreach (KeyValuePair<GameState, State> pair in new Dictionary<GameState, State>(Dragon.GameStates))
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel(pair.Key.ToString());
                Dragon.GameStates[pair.Key] = (State)EditorGUILayout.EnumPopup(pair.Value);
                EditorGUILayout.EndHorizontal();
            }
        }

    }
}