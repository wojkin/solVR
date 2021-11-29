using System.Collections.Generic;
using Tasks.TaskFailures.Utils;
using UnityEngine;

namespace Tasks.TaskConditions
{
    /// <summary>
    /// <inheritdoc/>
    /// Class checks condition of pushing object that ot will fall on the ground.
    /// </summary>
    public class TaskConditionPushToFall : TaskCondition
    {
        #region Serialized Fields

        /// <summary>List of objects that need to be pushed on ground.</summary>
        [SerializeField] private List<CollisionWithGround> boxCollisionsWithGround;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Subscribes to all needed event.
        /// </summary>
        private void OnEnable()
        {
            foreach (var collisionWithGround in boxCollisionsWithGround)
                collisionWithGround.CollidedWithGround += OnFell;
        }

        /// <summary>
        /// Unsubscribes from previously subscribed event.
        /// </summary>
        private void OnDisable()
        {
            foreach (var collisionWithGround in boxCollisionsWithGround)
                collisionWithGround.CollidedWithGround -= OnFell;
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Calls <see cref="TaskCondition.OnCompleted"/> if object that fell was the last one.
        /// </summary>
        private void OnFell(CollisionWithGround collided)
        {
            collided.CollidedWithGround -= OnFell; // unsubscribe from this object
            boxCollisionsWithGround.Remove(collided);
            if (boxCollisionsWithGround.Count == 0)
                OnCompleted();
        }

        #endregion
    }
}