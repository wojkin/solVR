using UnityEngine;

namespace Tasks.TaskConditions
{
    /// <summary>
    /// <inheritdoc/>
    /// Class checks condition of reaching a destination point by a robot.
    /// </summary>
    public class TaskConditionReachPoint : TaskCondition
    {
        #region Serialized Fields

        /// <summary>
        /// Transform of destination point.
        /// </summary>
        [Tooltip("Transform of destination point.")] [SerializeField]
        private Transform destination;

        /// <summary>
        /// Transform of robot that need to reach the destination.
        /// </summary>
        [Tooltip("Transform of robot that need to reach the destination.")] [SerializeField]
        private Transform robotPosition;

        /// <summary>
        /// Minimum robot distance from destination so that the condition is met.
        /// </summary>
        [Tooltip("Minimum robot distance from destination so that the condition is met.")] [SerializeField]
        private float minDistance = 1;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Checks if robot reached the destination point.
        /// If robot is in the destination point task condition is completed.
        /// </summary>
        private void Update()
        {
            if (!IsCompleted && Vector3.Distance(destination.position, robotPosition.position) <= minDistance)
                OnCompleted();
        }

        #endregion
    }
}