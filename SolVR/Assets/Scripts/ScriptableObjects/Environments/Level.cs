using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.Environments
{
    /// <summary>
    /// Stores information about a level as a scriptable object.
    /// </summary>
    [CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Environment/Level", order = 1)]
    public class Level : Environment
    {
        #region Serialized Fields

        /// <summary>List of blocks available in the level.</summary>
        [Tooltip("Blocks available in the level.")]
        public List<BlockData> blocks;

        /// <summary>The nest level after this one.</summary>
        [Tooltip("The nest level after this one.")]
        public Level nextLevel;

        #endregion
    }
}