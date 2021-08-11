using System.Collections;
using Exceptions;
using Robots.Actions;

namespace Robots.Commands
{
    /// <summary>
    /// A base class for all commands which can be executed by robots.
    /// </summary>
    /// <typeparam name="T">A robot class implementing the ICommandable interface.</typeparam>
    public abstract class Command<T> : ICommand where T : ICommandable
    {
        /// <summary>
        /// A coroutine which executes a command on a robot.
        /// If the robot can execute this command a function from a derived command class is executed. If the robot
        /// can't execute this command an exception is thrown.
        /// </summary>
        /// <param name="robot">The robot on which the command will be executed.</param>
        /// <returns>IEnumerator required for a coroutine.</returns>
        /// <exception cref="IncompatibleCommandException">An exception thrown when the robot can't execute the command
        /// (it doesn't implement the required interface).</exception>
        IEnumerator ICommand.Execute(ICommandable robot)
        {
            if (robot is T typedRobot)
            {
                yield return Execute(typedRobot);
            }
            else
            {
                Logger.OnLog("Unsupported command!");
                throw new IncompatibleCommandException();
            }
        }

        /// <summary>
        /// Coroutine which executes the command. Should be overriden in any deriving class in order to implement the
        /// command functionality.
        /// </summary>
        /// <param name="robot">The robot on which the command will be executed.</param>
        /// <returns>IEnumerator required for a coroutine.</returns>
        protected abstract IEnumerator Execute(T robot);
    }
}