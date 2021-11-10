using Exceptions;
using Robots;
using Robots.Actions;
using UnityEngine;

namespace Tasks.TaskFailures
{
    /// <summary>
    /// <inheritdoc/>
    /// Class fails task if robot crash.
    /// </summary>
    public class TaskFailureOnCrash : TaskFailure
    {
        #region Serialized Fields

        /// <summary>Robot that can't be crashed.</summary>
        [SerializeField] public Robot robot;

        #endregion

        #region Variables

        /// <summary>Crashable robot that can't be crash.</summary>
        private ICrashable _crashable;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initialize fields.
        /// </summary>
        private new void Awake()
        {
            base.Awake();
            if (robot is ICrashable crashableRobot)
                _crashable = crashableRobot;
            else
                throw new IncompatibleRobotException("Robot doesn't have ICrashable interface");
        }

        /// <summary>
        /// Subscribes to all needed events.
        /// </summary>
        private void OnEnable()
        {
            _crashable.AddListenerOnCrash(OnFailed);
        }

        /// <summary>
        /// Unsubscribes from all previously subscribed events.
        /// </summary>
        private void OnDisable()
        {
            _crashable.RemoveListenerOnCrash(OnFailed);
        }

        #endregion
    }
}