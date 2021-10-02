using System.Collections;
using DeveloperTools;
using Robots.Actions;

namespace Robots.Commands
{
    /// <summary>
    /// A class representing a command for moving a robot.
    /// </summary>
    public class MoveCommand : Command<IMovable>
    {
        #region Variables

        // <summary>Number of seconds the robot should move for when the command is executed.</summary>
        private readonly float _time;

        // <summary>Torque applied to the robots wheels, when the command is executed, given in Newton metres.</summary>
        private readonly float _torque;

        #endregion

        #region Custom Methods

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
            Logger.Log($"Started moving for {_time}s at {_torque}N⋅m.");
            yield return robot.Move(_time, _torque);
            Logger.Log($"Finished moving for {_time}s at {_torque}N⋅m.");
        }

        #endregion
    }
}