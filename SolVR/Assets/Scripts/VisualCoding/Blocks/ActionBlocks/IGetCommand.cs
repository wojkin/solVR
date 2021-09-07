using Robots.Commands;

namespace VisualCoding.Blocks.ActionBlocks
{
    /// <summary>
    /// Interface for getting a command.
    /// </summary>
    public interface IGetCommand
    {
        /// <summary>
        /// Creates and gets a command.
        /// </summary>
        /// <returns>A created command.</returns>
        public ICommand GetCommand();
    }
}