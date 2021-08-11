using System.Collections;
using Robots.Actions;

namespace Robots.Commands
{
    /// <summary>
    /// An interface for a command.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// A coroutine which executes a command on a robot.
        /// </summary>
        /// <param name="robot">Robot on which the command will be executed.</param>
        /// <returns>IEnumerator required for a coroutine.</returns>
        IEnumerator Execute(ICommandable robot);
    }
}