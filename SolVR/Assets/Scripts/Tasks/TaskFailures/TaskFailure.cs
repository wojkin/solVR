using ScriptableObjects.TaskDescriptions;
using UnityEngine;
using UnityEngine.Events;

namespace Tasks.TaskFailures
{
    /// <summary>
    /// Class representing condition that fails task when it's met.
    /// </summary>
    public abstract class TaskFailure : MonoBehaviour, ITaskCondition
    {
        #region Serialized Fields

        /// <summary>Task failure description with text information what means this task failure.</summary>
        [SerializeField] [Tooltip("Task failure description with text information what means this task failure.")]
        private TaskFailureDescription description;

        #endregion

        #region Variables

        /// <summary>Unity event which will be invoked when failure condition is met.</summary>
        private UnityEvent _failed = new UnityEvent();

        /// <summary>Task failure state flag representing if <see cref="_failed"/> event was called.</summary>
        public bool IsFailed { get; private set; }

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
        /// Invokes <see cref="_failed"/> event and sets flag <see cref="IsFailed"/> to true.
        /// </summary>
        protected void OnFailed()
        {
            IsFailed = true;
            _failed.Invoke();
        }

        #endregion

        #region ITaskCondition Methods

        /// <summary>
        /// Getter for <see cref="description"/>
        /// </summary>
        /// <returns>Task failure's description.</returns>
        public ITaskConditionDescription GetTaskConditionDescription() => description;

        /// <summary>
        /// Subscribes <see cref="listener"/> to <see cref="_failed"/> event.
        /// </summary>
        /// <param name="listener"><inheritdoc/></param>
        public void AddListener(UnityAction listener) => _failed.AddListener(listener);

        /// <summary>
        /// Unsubscribes <see cref="listener"/> to <see cref="_failed"/> event.
        /// </summary>
        /// <param name="listener"><inheritdoc/></param>
        public void RemoveListener(UnityAction listener) => _failed.RemoveListener(listener);

        #endregion
    }
}