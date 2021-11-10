using ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Tasks.TaskFailures
{
    /// <summary>
    /// Class representing condition that fails task when it's met.
    /// </summary>
    public abstract class TaskFailure : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>Unity event which will be invoked when failure condition is met.</summary>
        public UnityEvent failed = new UnityEvent();

        /// <summary>Task failure description with text information what means this task failure.</summary>
        [SerializeField] [Tooltip("Task failure description with text information what means this task failure.")]
        private TaskFailureDescription description;

        #endregion

        #region Variables

        /// <summary>Task failure state flag representing if <see cref="failed"/> event was called.</summary>
        public bool IsFailed { get; private set; }

        /// <summary><inheritdoc cref="description"/></summary>
        public TaskFailureDescription Description => description;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initialize fields.
        /// </summary>
        protected void Awake()
        {
            IsFailed = false;
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Invokes <see cref="failed"/> event and sets flag <see cref="IsFailed"/> to true.
        /// </summary>
        public void OnFailed()
        {
            IsFailed = true;
            failed.Invoke();
        }

        #endregion
    }
}