using System.Collections;

namespace Robots.Actions
{
    /// <summary>
    /// An interface for a robot, which can move.
    /// </summary>
    public interface IMovable : ICommandable
    {
        #region Custom Methods

        /// <summary>
        /// A coroutine which moves the robot for a given amount of time with a given torque.
        /// </summary>
        /// <param name="time">The number of seconds the robot should move for.</param>
        /// <param name="torque">The torque applied to the robots wheels, given in Newton metres.</param>
        /// <returns>IEnumerator required for a coroutine.</returns>
        IEnumerator Move(float time, float torque);

        #endregion
    }
}