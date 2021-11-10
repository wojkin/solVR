using ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Tasks.TaskConditions
{
    /// <summary>
    /// Class representing one of task condition to complete the task.
    /// </summary>
    public abstract class TaskCondition : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>Unity event which will be invoked when condition is met.</summary>
        public UnityEvent completed = new UnityEvent();

        /// <summary>Task condition description with text information what means this task condition.</summary>
        [SerializeField] [Tooltip("Task condition description with text information what means this task condition.")]
        private TaskConditionDescription description;

        #endregion

        #region Variables

        /// <summary>
        /// Flag representing completion of task condition, true if <see cref="completed"/> event was called.
        /// </summary>
        public bool IsCompleted { get; private set; }

        /// <summary><inheritdoc cref="description"/></summary>
        public TaskConditionDescription Description => description;

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
        /// Invokes <see cref="completed"/> event and sets flag <see cref="IsCompleted"/> to true.
        /// </summary>
        public void OnCompleted()
        {
            IsCompleted = true;
            completed.Invoke();
        }

        #endregion
    }
}