using System.Collections;
using Robots.Actions;

namespace Robots.Commands
{
    /// <summary>
    /// A class representing a command for moving a robot.
    /// </summary>
    public class MoveCommand : Command<IMovable>
    {
        private readonly float _time; // the number of seconds the robot should move for when the command is executed.

        private readonly float _torque; // the torque applied to the robots wheels, when the command is executed, given
        // in Newton metres.

        /// <summary>
        /// A constructor for a move command.
        /// </summary>
        /// <param name="time">The number of seconds the robot should move for when the command is executed.</param>
        /// <param name="torque">The torque applied to the robots wheels, when the command is executed, given in Newton
        /// metres.</param>
        public MoveCommand(float time, float torque)
        {
            _time = time;
            _torque = torque;
        }

        /// <summary>
        /// A coroutine for executing the move command on the robot.
        /// </summary>
        /// <param name="robot">The robot, which will be moved.</param>
        /// <returns>IEnumerator required for a coroutine.</returns>
        protected override IEnumerator Execute(IMovable robot)
        {
            Logger.OnLog($"Started moving for {_time}s at {_torque}N⋅m.");
            yield return robot.Move(_time, _torque);
            Logger.OnLog($"Finished moving for {_time}s at {_torque}N⋅m.");
        }
    }
}