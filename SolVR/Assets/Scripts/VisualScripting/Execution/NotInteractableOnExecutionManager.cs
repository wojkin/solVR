using System.Collections.Generic;
using UI.Interactable;
using UnityEngine;

namespace VisualScripting.Execution
{
    /// <summary>
    /// Class responsible for managing <see cref="Interactable"/> objects to be not interactable while
    /// script is executing.
    /// </summary>
    public class NotInteractableOnExecutionManager : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>List of object that can't be interactable while execution is running.</summary>
        [SerializeField] private List<Interactable> notInteractableWhileExecuting;

        /// <summary>Manager that executes the script.</summary>
        [SerializeField] private ExecutionManager executionManager;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Subscribes to all needed events.
        /// </summary>
        private void OnEnable()
        {
            executionManager.ExecutionStarted += SetNotInteractable;
            executionManager.ExecutionEnded += SetInteractable;
        }

        /// <summary>
        /// Unsubscribes from previously subscribed events.
        /// </summary>
        private void OnDisable()
        {
            executionManager.ExecutionStarted -= SetNotInteractable;
            executionManager.ExecutionEnded += SetInteractable;
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Sets all object from <see cref="notInteractableWhileExecuting"/> list to not interactable.
        /// </summary>
        private void SetNotInteractable()
        {
            foreach (var interactable in notInteractableWhileExecuting) interactable.SetInteractable(false);
        }

        /// <summary>
        /// Sets all object from <see cref="notInteractableWhileExecuting"/> list to interactable.
        /// </summary>
        private void SetInteractable()
        {
            foreach (var interactable in notInteractableWhileExecuting) interactable.SetInteractable(true);
        }

        #endregion
    }
}