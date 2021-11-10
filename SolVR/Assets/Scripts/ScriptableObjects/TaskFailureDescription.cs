using UnityEngine;

namespace ScriptableObjects
{
    /// <summary>
    /// Stores information about a task failure as scriptable object.
    /// </summary>
    [CreateAssetMenu(fileName = "TaskFailureDescription",
        menuName = "ScriptableObjects/TaskDescriptions/TaskFailureDescription",
        order = 2)]
    public class TaskFailureDescription : ScriptableObject
    {
        #region Serialized Fields

        /// <summary>
        /// Description of the task failure.
        /// </summary>
        [Tooltip("Description of task failure.")] [TextArea]
        public string description;

        #endregion
    }
}