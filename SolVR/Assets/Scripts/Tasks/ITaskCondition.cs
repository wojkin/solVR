using ScriptableObjects.TaskDescriptions;
using UnityEngine.Events;

namespace Tasks
{
    /// <summary>
    /// Interface used to get handle task's conditions events and getting description.
    /// </summary>
    public interface ITaskCondition
    {
        #region Custom Methods

        /// <summary>
        /// Getter for description.
        /// </summary>
        /// <returns>Task condition's description.</returns>
        ITaskConditionDescription GetTaskConditionDescription();

        /// <summary>
        /// Subscribes <see cref="listener"/> to event, that's invoked when condition is met.
        /// </summary>
        /// <param name="listener">Unity action that is passed to subscribe to an event.</param>
        void AddListener(UnityAction listener);

        /// <summary>
        /// Unsubscribes <see cref="listener"/> to event, that's invoked when condition is met.
        /// </summary>
        /// <param name="listener">Unity action that is passed to unsubscribe from subscribed event.</param>
        void RemoveListener(UnityAction listener);

        #endregion
    }
}