using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects.TaskDescriptions
{
    /// <summary>
    /// Stores information about a task as scriptable object.
    /// </summary>
    [CreateAssetMenu(fileName = "TaskDescription", menuName = "ScriptableObjects/TaskDescription", order = 4)]
    public class TaskDescription : ScriptableObject
    {
        #region Serialized Fields

        /// <summary>
        /// Description of the task.
        /// </summary>
        [Tooltip("Description of the task.")] [TextArea]
        public string description;

        /// <summary>
        /// List of tasks conditions.
        /// </summary>
        [SerializeField] public List<TaskConditionDescription> conditions = new List<TaskConditionDescription>();

        /// <summary>
        /// List of tasks failures.
        /// </summary>
        [SerializeField] public List<TaskFailureDescription> failure = new List<TaskFailureDescription>();

        #endregion
    }
}