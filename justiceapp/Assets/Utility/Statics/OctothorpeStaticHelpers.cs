using UnityEngine;

namespace Utility
{
    public static class OctothorpeStaticHelpers
    {
        public static void RemoveAllChildren(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                MonoBehaviour.Destroy(child.gameObject);
            }
        }
    }
}