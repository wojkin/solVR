using Robots.Actions;

namespace Robots.Commands
{
    /// <summary>
    /// An interface for a command.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Executes a command on a robot.
        /// </summary>
        /// <param name="robot">Robot on which the command will be executed.</param>
        void Execute(ICommandable robot);
    }
}