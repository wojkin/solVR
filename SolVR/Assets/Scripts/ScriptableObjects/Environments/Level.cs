using ScriptableObjects.TaskDescriptions;
using UnityEngine;

namespace ScriptableObjects.Environments
{
    /// <summary>
    /// Stores information about a level as scriptable object.
    /// </summary>
    [CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Environment/Level", order = 1)]
    public class Level : Environment
    {
        #region Serialized Fields

        /// <summary>
        /// Description of task on the level.
        /// </summary>
        [Tooltip("Description of task on the level.")]
        public TaskDescription levelTaskDescription;

        #endregion
    }
}