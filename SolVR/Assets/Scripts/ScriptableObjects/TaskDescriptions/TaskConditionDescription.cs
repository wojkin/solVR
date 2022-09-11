using UnityEngine;

namespace ScriptableObjects.TaskDescriptions
{
    /// <summary>
    /// Stores information about a task condition as scriptable object.
    /// </summary>
    [CreateAssetMenu(fileName = "TaskConditionDescription",
        menuName = "ScriptableObjects/TaskDescriptions/TaskConditionDescription",
        order = 1)]
    public class TaskConditionDescription : ScriptableObject, ITaskConditionDescription
    {
        #region Serialized Fields

        /// <summary>Description of the task condition.</summary>
        [Tooltip("Description of the task condition.")] [TextArea]
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