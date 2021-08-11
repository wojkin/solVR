using System.Collections;
using Robots.Enums;

namespace Robots.Actions
{
    /// <summary>
    /// An interface for a robot, which can turn.
    /// </summary>
    public interface ITurnable : ICommandable
    {
        /// <summary>
        /// Turns the robot for a given amount of time with a given torque.
        /// </summary>
        /// <param name="time">The number of seconds the robot should turn for.</param>
        /// <param name="torque">The torque applied to the robots wheels, given in Newton metres.</param>
        /// <param name="direction">The direction in which the robot should turn.</param>
        /// <returns>IEnumerator required for a coroutine.</returns>
        IEnumerator Turn(float time, float torque, TurnDirection direction);
    }
}