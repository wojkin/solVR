using System.Collections.Generic;
using Patterns;
using Robots;
using ScriptableObjects;
using Tasks;
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
        [SerializeField] public List<BlockData> blockData;

        /// <summary>Robot available in the level.</summary>
        [SerializeField] public Robot robot;

        /// <summary>Task available in the level.</summary>
        [SerializeField] public Task task;

        #endregion

        #region Variables

        /// <summary>Delegate for data initialize.</summary>
        public delegate void DataInitialize();

        /// <summary>Event invoked to notify listeners that level data is initialized.</summary>
        public event DataInitialize DataInitialized;

        #endregion

        #region Custom Methods

        /// <summary>
        /// Invoke <see cref="DataInitialized"/> event.
        /// </summary>
        public void LevelDataInitialized()
        {
            DataInitialized?.Invoke();
        }

        #endregion
    }
}