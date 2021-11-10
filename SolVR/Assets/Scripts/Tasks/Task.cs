using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using Tasks.TaskConditions;
using Tasks.TaskFailures;
using UnityEngine;
using UnityEngine.Events;

namespace Tasks
{
    /// <summary>
    /// Class representing task with conditions that need to be met to complete the task and failures that can't be met.
    /// </summary>
    public class Task : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>Unity event which will be invoked when all condition are completed.</summary>
        public UnityEvent completed = new UnityEvent();

        /// <summary>Unity event which will be invoked when at least one failure condition is met.</summary>
        public UnityEvent failed = new UnityEvent();

        /// <summary>List of all condition that need to be met to complete the task.</summary>
        public List<TaskCondition> conditions = new List<TaskCondition>();

        /// <summary>List of all failures that can't be failed to complete the task.</summary>
        public List<TaskFailure> failures = new List<TaskFailure>();

        /// <summary>Task description with text information what this task.</summary>
        [SerializeField] [Tooltip("Task description with text information what this task.")]
        private TaskDescription description;

        #endregion

        #region Variables

        /// <summary><inheritdoc cref="description"/></summary>
        public TaskDescription Description => description;

        /// <summary>State of the task that represent it's completion.</summary>
        public TaskCompletionState State { get; private set; }

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initialize fields.
        /// </summary>
        protected void Awake()
        {
            State = TaskCompletionState.NotCompleted;
        }

        /// <summary>
        /// Subscribes to all needed events.
        /// </summary>
        void OnEnable()
        {
            completed.AddListener(() => State = TaskCompletionState.Completed);
            failed.AddListener(() => State = TaskCompletionState.Failed);
            foreach (var condition in conditions) condition.completed.AddListener(OnTaskConditionCompleted);

            foreach (var failure in failures) failure.failed.AddListener(OnTaskFailureFailed);
        }

        /// <summary>
        /// Unsubscribes from all previously subscribed events.
        /// </summary>
        void OnDisable()
        {
            completed.RemoveAllListeners();
            failed.RemoveAllListeners();
            foreach (var condition in conditions) condition.completed.RemoveListener(OnTaskConditionCompleted);

            foreach (var failure in failures) failure.failed.RemoveListener(OnTaskFailureFailed);
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Invokes <see cref="completed"/> event and sets <see cref="State"/> of the task.
        /// </summary>
        private void OnTaskConditionCompleted()
        {
            if (State != TaskCompletionState.Failed && conditions.All(condition => condition.IsCompleted))
                completed.Invoke();
        }

        /// <summary>
        /// Invokes <see cref="failed"/> event and sets <see cref="State"/> of the task.
        /// </summary>
        private void OnTaskFailureFailed()
        {
            State = TaskCompletionState.Failed;
            failed.Invoke();
        }

        #endregion
    }
}