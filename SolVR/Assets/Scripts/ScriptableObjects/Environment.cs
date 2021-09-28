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

        /// <summary>name of the scene</summary>
        [Tooltip("Name of the scene.")] public string environmentName;

        /// <summary>reference to addressable scene</summary>
        [Tooltip("Reference to addressable scene.")]
        public AssetReference scene;

        #endregion
    }
}