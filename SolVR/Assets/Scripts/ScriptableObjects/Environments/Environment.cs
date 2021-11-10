using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ScriptableObjects.Environments
{
    /// <summary>
    /// Stores information about a scene as <see cref="ScriptableObject"/>.
    /// </summary>
    [CreateAssetMenu(fileName = "Environment", menuName = "ScriptableObjects/Environment", order = 1)]
    public class Environment : ScriptableObject
    {
        #region Serialized Fields

        /// <summary>Name of the scene.</summary>
        [Tooltip("Name of the scene.")] public string environmentName;

        /// <summary>Reference to addressable scene.</summary>
        [Tooltip("Reference to addressable scene.")]
        public AssetReference scene;

        #endregion
    }
}