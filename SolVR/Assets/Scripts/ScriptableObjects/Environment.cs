using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ScriptableObjects
{
    /// <summary>
    /// Stores information about a scene as scriptable object.
    /// </summary>
    [CreateAssetMenu(fileName = "Environment", menuName = "ScriptableObjects/Environment", order = 1)]
    public class Environment : ScriptableObject
    {
        #region Serialized Fields

        // Name of the scene
        public string environmentName;

        // Reference to addressable scene
        public AssetReference scene;

        #endregion
    }
}