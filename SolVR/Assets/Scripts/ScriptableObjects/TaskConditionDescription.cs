using UnityEngine;

namespace ScriptableObjects
{
    /// <summary>
    /// Stores information about a task condition as scriptable object.
    /// </summary>
    [CreateAssetMenu(fileName = "TaskConditionDescription",
        menuName = "ScriptableObjects/TaskDescriptions/TaskConditionDescription",
        order = 1)]
    public class TaskConditionDescription : ScriptableObject
    {
        #region Serialized Fields

        /// <summary>
        /// Description of the task condition.
        /// </summary>
        [Tooltip("Description of the task condition.")] [TextArea]
        public string description;

        #endregion
    }
}