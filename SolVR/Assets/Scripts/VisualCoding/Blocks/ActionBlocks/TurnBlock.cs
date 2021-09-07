using Robots.Commands;
using Robots.Enums;

namespace VisualCoding.Blocks.ActionBlocks
{
    /// <summary>
    /// Class representing a block and creating a turn command.
    /// </summary>
    public class TurnBlock: Block, IGetCommand
    {
        public float Time { get; set; } // the number of seconds turn command is executed

        public float Torque { get; set; } // the torque to the turn command

        public TurnDirection Direction { get; set; } // the direction in which the robot should turn
        
        /// <summary>
        /// Creates and gets a turn command.
        /// </summary>
        /// <returns>A created TurnCommand with time, torque and direction given as constructor parameters.</returns>
        public ICommand GetCommand()
        {
            return new TurnCommand(Time, Torque, Direction);
        }
    }
}