using System.Collections;
using Robots.Actions;
using Robots.Enums;

namespace Robots.Commands
{
    /// <summary>
    /// A class representing a command for turning robots wheels.
    /// </summary>
    public class TurnCommand : Command<ITurnable>
    {
        private readonly TurnDirection _direction; // the direction in which the robots wheels should turn
        private readonly int _angle; // the steer angle of the wheels around the local vertical axis

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
            Logger.OnLog($"Wheels turned {_angle} degrees {_direction.ToString()}.");
        }
    }
}