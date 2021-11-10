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

        #region Variables

        /// <summary>Default message for task condition state</summary>
        private const string DefaultStateMessage = "No failure";

        /// <summary>Message for task condition state, when condition is met.</summary>
        private const string ConditionMetStateMessage = "Failed";

        #endregion

        #region ITaskConditionDescription Methods

        /// <summary>
        /// Getter for <see cref="description"/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public string GetDescription() => description;

        /// <inheritdoc />
        public string GetDefaultStateMessage() => DefaultStateMessage;

        /// <inheritdoc />
        public string GetConditionMetStateMessage() => ConditionMetStateMessage;

        #endregion
    }
}