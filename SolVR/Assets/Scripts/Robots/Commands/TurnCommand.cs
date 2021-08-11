using System.Collections;
using Robots.Actions;
using Robots.Enums;

namespace Robots.Commands
{
    /// <summary>
    /// A class representing a command for turning a robot.
    /// </summary>
    public class TurnCommand : Command<ITurnable>
    {
        private readonly float _time; // the number of seconds the robot should turn for when the command is executed.

        private readonly float _torque; // the torque applied to the robots wheels, when the command is executed, given
        // in Newton metres.

        private readonly TurnDirection _direction; // the direction in which the robot should turn.

        /// <summary>
        /// A constructor for a turn command.
        /// </summary>
        /// <param name="time">The number of seconds the robot should turn for when the command is executed.</param>
        /// <param name="torque">The torque applied to the robots wheels, when the command is executed, given in Newton
        /// metres.</param>
        /// <param name="direction">The direction in which the robot should turn.</param>
        public TurnCommand(float time, float torque, TurnDirection direction)
        {
            _time = time;
            _torque = torque;
            _direction = direction;
        }

        /// <summary>
        /// A coroutine for executing the turn command on the robot.
        /// </summary>
        /// <param name="robot">The robot, which will be turned.</param>
        /// <returns>IEnumerator required for a coroutine.</returns>
        protected override IEnumerator Execute(ITurnable robot)
        {
            Logger.OnLog($"Started turning {_direction.ToString()} for {_time}s at {_torque}N⋅m.");
            yield return robot.Turn(_time, _torque, _direction);
            Logger.OnLog($"Finished turning {_direction.ToString()} for {_time}s at {_torque}N⋅m.");
        }
    }
}