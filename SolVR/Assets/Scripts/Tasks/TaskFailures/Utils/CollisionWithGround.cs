using UnityEngine;

namespace Tasks.TaskFailures.Utils
{
    /// <summary>
    /// Class responsible for invoking an event if object collides with ground.
    /// </summary>
    public class CollisionWithGround : MonoBehaviour
    {
        #region Variables

        /// <summary>A delegate for collide with ground.</summary>
        public delegate void CollideWithGround();

        /// <summary>An event invoked on object collision with ground.</summary>
        public event CollideWithGround CollidedWithGround;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Invokes <see cref="CollidedWithGround"/> event if <see cref="other"/> is ground.
        /// </summary>
        /// <param name="other">The object with which gameObject collided</param>
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag.Equals("Ground")) CollidedWithGround?.Invoke();
        }

        #endregion
    }
}