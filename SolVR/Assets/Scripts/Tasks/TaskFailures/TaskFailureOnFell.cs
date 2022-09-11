using System.Collections.Generic;
using Tasks.TaskFailures.Utils;
using UnityEngine;

namespace Tasks.TaskFailures
{
    /// <summary>
    /// <inheritdoc/>
    /// Class fails task if any of <see cref="boxCollisionsWithGround"/> will have collision with ground.
    /// </summary>
    public class TaskFailureOnFell : TaskFailure
    {
        #region Serialized Fields

        /// <summary>List of objects that will invoke task failure if they collide with ground.</summary>
        [SerializeField] private List<CollisionWithGround> boxCollisionsWithGround;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Subscribes to all needed events.
        /// </summary>
        private void OnEnable()
        {
            foreach (var collisionWithGround in boxCollisionsWithGround)
                collisionWithGround.CollidedWithGround += OnFell;
        }

        /// <summary>
        /// Unsubscribes from previously subscribed collision with ground event if task failure not failed.
        /// </summary>
        private void OnDisable()
        {
            if (!IsFailed) UnsubscribeFromCollisions();
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Calls <see cref="TaskFailure.OnFailed"/> and
        /// unsubscribes from previously subscribed collision with ground event if task failure failed.
        /// </summary>
        private void OnFell(CollisionWithGround collided)
        {
            OnFailed();
            UnsubscribeFromCollisions(); // Only one object fell is needed to fail the task.
        }

        /// <summary>
        /// Unsubscribes from previously subscribed collision with ground event.
        /// </summary>
        private void UnsubscribeFromCollisions()
        {
            foreach (var collisionWithGround in boxCollisionsWithGround)
                collisionWithGround.CollidedWithGround -= OnFell;
        }

        #endregion
    }
}