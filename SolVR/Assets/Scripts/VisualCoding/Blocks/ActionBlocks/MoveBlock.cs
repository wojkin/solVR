using Robots.Commands;

namespace VisualCoding.Blocks.ActionBlocks
{
    /// <summary>
    /// Class representing a block and creating a move command.
    /// </summary>
    public class MoveBlock : Block, IGetCommand
    {
        public float Time {get; set; } // the number of seconds move command is executed
        
        public float Torque {get; set; }  // the torque to the move command

        /// <summary>
        /// Creates and gets a move command.
        /// </summary>
        /// <returns>A created MoveCommand with time and torque given as constructor parameters.</returns>
        public ICommand GetCommand()
        {
            return new MoveCommand(Time, Torque);
        }
    }
}