using Robots.Actions;

namespace Robots.Commands
{
    /// <summary>
    /// A class representing a command for turning a robot.
    /// </summary>
    public class TurnCommand : Command<ITurnable>
    {
        private readonly float _time; // The number of seconds the robot should turn for when the command is executed.

        private readonly float _speed; // The speed at which the robot should turn at, given in radians per second when
        // the command is executed.

        /// <summary>
        /// A constructor for a turn command.
        /// </summary>
        /// <param name="time">The number of seconds the robot should turn for when the command is executed.</param>
        /// <param name="speed">The speed at which the robot should turn at, given in radians per second when the command
        /// is executed.</param>
        public TurnCommand(float time, float speed)
        {
            _time = time;
            _speed = speed;
        }

        /// <summary>
        /// A command for turning the robot.
        /// </summary>
        /// <param name="robot">Robot, which will be turned.</param>
        protected override void Execute(ITurnable robot)
        {
            robot.Turn(_time, _speed);
        }
    }
}