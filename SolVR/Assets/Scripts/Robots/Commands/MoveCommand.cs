using Robots.Actions;

namespace Robots.Commands
{
    /// <summary>
    /// A class representing a command for moving a robot.
    /// </summary>
    public class MoveCommand : Command<IMovable>
    {
        private readonly float _time; // The number of seconds the robot should move for when the command is executed.

        private readonly float _speed; // The speed at which the robot should move at, given in meters per second when
        // the command is executed.

        /// <summary>
        /// A constructor for a move command.
        /// </summary>
        /// <param name="time">The number of seconds the robot should move for when the command is executed.</param>
        /// <param name="speed">The speed at which the robot should move at, given in meters per second when the command
        /// is executed.</param>
        public MoveCommand(float time, float speed)
        {
            _time = time;
            _speed = speed;
        }

        /// <summary>
        /// A command for moving the robot.
        /// </summary>
        /// <param name="robot">Robot, which will be moved.</param>
        protected override void Execute(IMovable robot)
        {
            robot.Move(_time, _speed);
        }
    }
}