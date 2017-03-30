using UnityEngine;

namespace Utility.Singleton
{
    public static class MethodExtensionForMonoBehaviourTransform
    {
        /// <summary>
        /// Gets or add a component. Usage example:
        /// BoxCollider boxCollider = transform.GetOrAddComponent<BoxCollider>();
        /// </summary>
        public static T GetOrAddComponent<T>(this Component child) where T : Component
        {
            T result = child.GetComponent<T>() ?? child.gameObject.AddComponent<T>();
            return result;
        }
    }
}