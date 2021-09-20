using Robots.Commands;
using Robots.Enums;

namespace VisualCoding.Blocks.ActionBlocks
{
    /// <summary>
    /// Class representing a turn block which creates a turn command.
    /// </summary>
    public class TurnBlock : Block, IGetCommand
    {
        public TurnDirection Direction { get; set; } // the direction in which the robot should turn
        public int Angle { get; set; } // the steer angle of the wheels around the local vertical axis

        /// <summary>
        /// Creates and returns a turn command.
        /// </summary>
        /// <returns>A turn command with direction and angle as configured in the block.</returns>
        public ICommand GetCommand()
        {
            return new TurnCommand(Direction, Angle);
        }
    }
}