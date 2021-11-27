using Robots;
using Tasks;
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
        [SerializeField] private Robot robot;

        /// <summary>Task in the level.</summary>
        [SerializeField] private Task task;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Pass all data to <see cref="PersistentLevelData"/> on level initialize.
        /// </summary>
        private void Start()
        {
            PersistentLevelData.Instance.OnLevelLoad(robot, task);
        }

        #endregion
    }
}