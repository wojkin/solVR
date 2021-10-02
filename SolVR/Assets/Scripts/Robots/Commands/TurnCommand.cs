using System.Collections;
using DeveloperTools;
using Robots.Actions;
using Robots.Enums;

namespace Robots.Commands
{
    /// <summary>
    /// A class representing a command for turning robots wheels.
    /// </summary>
    public class TurnCommand : Command<ITurnable>
    {
        #region Variables

        // <summary>Direction in which the robots wheels should turn.</summary>
        private readonly TurnDirection _direction;

        /// <summary>Steer angle of the wheels around the local vertical axis.</summary>
        private readonly int _angle;

        #endregion

        #region Custom Methods

        /// <summary>
        /// A constructor for a turn command.
        /// </summary>
        /// <param name="direction">The direction in which the robots wheels should turn.</param>
        /// <param name="angle">The steer angle of the wheels around the local vertical axis.</param>
        public TurnCommand(TurnDirection direction, int angle)
        {
            _direction = direction;
            _angle = angle;
        }

        /// <summary>
        /// A coroutine for executing the turn command on the robot.
        /// </summary>
        /// <param name="robot">The robot, which wheels will be turned.</param>
        /// <returns>IEnumerator required for a coroutine.</returns>
        protected override IEnumerator Execute(ITurnable robot)
        {
            yield return robot.Turn(_direction, _angle);
            Logger.Log($"Wheels turned {_angle} degrees {_direction.ToString()}.");
        }

        #endregion
    }
}