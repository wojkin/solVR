using Robots;
using UnityEngine;

namespace Levels
{
    /// <summary>
    /// Component for passing data from level scene to <see cref="PersistentLevelData"/>.
    /// </summary>
    public class LevelInitializer : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>Robot in the level.</summary>
        [SerializeField]
        private Robot robot;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Pass all data to <see cref="PersistentLevelData"/>.
        /// </summary>
        private void Awake()
        {
            PersistentLevelData.Instance.robot = robot;
        }

        #endregion
    }
}