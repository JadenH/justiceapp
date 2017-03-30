using UnityEditor;
using Utility;

[CustomEditor(typeof(DefaultGameState))]
public class DefaultGameStatesEditor : Editor
{
    [MenuItem("Assets/Create/Default Game States")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<DefaultGameState>();
    }
}