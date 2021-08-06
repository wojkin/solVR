namespace Robots.Actions
{
    /// <summary>
    /// An interface for a robot, which can move.
    /// </summary>
    public interface IMovable : ICommandable
    {
        /// <summary>
        /// Moves the robot for a given amount of time with a given speed.
        /// </summary>
        /// <param name="time">The number of seconds the robot should move for.</param>
        /// <param name="speed">The speed at which the robot should move at, given in meters per second.</param>
        void Move(float time, float speed);
    }
}
