using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DragonBehaviour), true)]
public class DragonBehaviourEditor : DragonEditor
{
    public override void OnInspectorGUI()
    {
        DragonBehaviour behaviour = (DragonBehaviour)target;
        var gameStates = behaviour.GetType().GetCustomAttributes(typeof(GameStateAttribute), true)
            .Cast<GameStateAttribute>().Select(attribute => attribute.GameState).ToList();

        GUI.enabled = false;
        GUILayout.Space(10f);
        if (gameStates.Any())
        {
            GUILayout.BeginHorizontal();
            var gameStatesString = gameStates.Aggregate("", (current, gameState) => current + (gameState + ", "));
            GUILayout.Label("GameStates", GUILayout.Width(EditorGUIUtility.labelWidth));
            GUILayout.Label(gameStatesString.TrimEnd(',', ' '));
            GUILayout.EndHorizontal();
        }
        else
        {
            GUILayout.Label("No GameState Attributes", new GUIStyle { alignment = TextAnchor.MiddleCenter });
        }
        GUILayout.Space(10f);
        GUI.enabled = true;

        DrawDefaultInspector();
    }
}