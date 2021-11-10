using ScriptableObjects.TaskDescriptions;
using UnityEngine;
using UnityEngine.Events;

namespace Tasks.TaskConditions
{
    /// <summary>
    /// Class representing one of task condition to complete the task.
    /// </summary>
    public abstract class TaskCondition : MonoBehaviour, ITaskCondition
    {
        #region Serialized Fields

        /// <summary>Task condition description with text information what means this task condition.</summary>
        [SerializeField] [Tooltip("Task condition description with text information what means this task condition.")]
        private TaskConditionDescription description;

        #endregion

        #region Variables

        /// <summary>Unity event which will be invoked when condition is met.</summary>
        private UnityEvent _completed = new UnityEvent();

        /// <summary>
        /// Flag representing completion of task condition, true if <see cref="_completed"/> event was called.
        /// </summary>
        public bool IsCompleted { get; private set; }

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initialize fields.
        /// </summary>
        protected void Awake()
        {
            IsCompleted = false;
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Invokes <see cref="_completed"/> event and sets flag <see cref="IsCompleted"/> to true.
        /// </summary>
        protected void OnCompleted()
        {
            IsCompleted = true;
            _completed.Invoke();
        }

        #endregion

        #region ITaskCondition Methods

        /// <summary>
        /// Getter for <see cref="description"/>
        /// </summary>
        /// <returns>Task condition's description.</returns>
        public ITaskConditionDescription GetTaskConditionDescription() => description;

        /// <summary>
        /// Subscribes <see cref="listener"/> to <see cref="_completed"/> event.
        /// </summary>
        /// <param name="listener"><inheritdoc/></param>
        public void AddListener(UnityAction listener) => _completed.AddListener(listener);

        /// <summary>
        /// Unsubscribes <see cref="listener"/> from <see cref="_completed"/> event.
        /// </summary>
        /// <param name="listener"><inheritdoc/></param>
        public void RemoveListener(UnityAction listener) => _completed.RemoveListener(listener);

        #endregion
    }
}