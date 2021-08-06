namespace Robots.Actions
{
    /// <summary>
    /// An interface for a robot, which can turn.
    /// </summary>
    public interface ITurnable : ICommandable
    {
    /// <summary>
    /// Turns the robot for a given amount of time with a given speed.
    /// </summary>
    /// <param name="time">The number of seconds the robot should turn for.</param>
    /// <param name="speed">The speed at which the robot should turn at, given in radians per second.</param>
        void Turn(float time, float speed);
    }
}