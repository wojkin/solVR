using System.Collections;
using Robots.Enums;

namespace Robots.Actions
{
    /// <summary>
    /// An interface for a robot, which can turn its wheels.
    /// </summary>
    public interface ITurnable : ICommandable
    {
        #region Custom Methods

        /// <summary>
        /// Turns the robots wheels in a direction to an angle around the local vertical axis.
        /// </summary>
        /// <param name="direction">The direction in which the robots wheels should turn.</param>
        /// <param name="angle">The steer angle of the wheels around the local vertical axis.</param>
        /// <returns>IEnumerator required for a coroutine.</returns>
        IEnumerator Turn(TurnDirection direction, int angle);

        #endregion
    }
}