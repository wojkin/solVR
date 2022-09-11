using UnityEngine;

namespace ScriptableObjects.TaskDescriptions
{
    /// <summary>
    /// Stores information about a task failure as scriptable object.
    /// </summary>
    [CreateAssetMenu(fileName = "TaskFailureDescription",
        menuName = "ScriptableObjects/TaskDescriptions/TaskFailureDescription",
        order = 2)]
    public class TaskFailureDescription : ScriptableObject, ITaskConditionDescription
    {
        #region Serialized Fields

        /// <summary>
        /// Description of the task failure.
        /// </summary>
        [Tooltip("Description of task failure.")] [TextArea]
        public string description;

        #endregion

        #region ITaskConditionDescription Methods

        /// <summary>
        /// Getter for <see cref="description"/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public string GetDescription() => description;

        #endregion
    }
}