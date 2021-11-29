using System.Collections.Generic;
using Levels;
using UnityEngine;
using VisualScripting.Blocks.Properties;

namespace VisualScripting.Blocks
{
    /// <summary>
    /// Class responsible for resetting blocks on reloading the level.
    /// </summary>
    public class BlockManager : MonoBehaviour
    {
        #region Built-in Methods

        /// <summary>
        /// Subscribes to all needed events.
        /// </summary>
        private void OnEnable()
        {
            PersistentLevelData.Instance.LevelLoaded += ResetBlocks;
        }

        /// <summary>
        /// Unsubscribes from previously subscribed events.
        /// </summary>
        private void OnDisable()
        {
            if (PersistentLevelData.Instance != null)
                PersistentLevelData.Instance.LevelLoaded -= ResetBlocks;
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Resets blocks with <see cref="IResettable"/> interface.
        /// </summary>
        private void ResetBlocks()
        {
            IEnumerable<IResettable> blocksToReset = GetComponentsInChildren<IResettable>();
            foreach (var block in blocksToReset) block.Reset();
        }

        #endregion
    }
}