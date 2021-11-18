using System.Collections.Generic;
using Patterns;
using Robots;
using ScriptableObjects;
using UnityEngine;

namespace Levels
{
    /// <summary>
    /// Class for passing level data between scenes.
    /// </summary>
    public class PersistentLevelData : Singleton<PersistentLevelData>
    {
        #region Serialized Fields

        /// <summary><see cref="BlockData"/> of blocks available in the level.</summary>
        [SerializeField]
        public List<BlockData> blockData;

        /// <summary>Robot available in the level.</summary>
        [SerializeField]
        public Robot robot;

        #endregion
    }
}