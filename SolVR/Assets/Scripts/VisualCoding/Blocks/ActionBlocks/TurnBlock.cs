using Robots.Commands;
using Robots.Enums;
using UnityEngine;

namespace VisualCoding.Blocks.ActionBlocks
{
    /// <summary>
    /// Class representing a turn block which creates a turn command.
    /// </summary>
    public class TurnBlock : Block, IGetCommand
    {
        #region Serialized Fields

        [SerializeField] [Tooltip("The direction in which the robot should turn.")]
        private TurnDirection direction;

        [SerializeField] [Tooltip("The steer angle of the wheels around the local vertical axis.")]
        private int angle;

        #endregion

        #region Variables

        public TurnDirection Direction
        {
            get => direction;
            set => direction = value;
        } // the direction in which the robot should turn

        public int Angle
        {
            get => angle;
            set => angle = value;
        } // the steer angle of the wheels around the local vertical axis

        #endregion

        #region IGetCommand Methods

        /// <summary>
        /// Creates and returns a turn command.
        /// </summary>
        /// <returns>A turn command with direction and angle as configured in the block.</returns>
        public ICommand GetCommand()
        {
            return new TurnCommand(Direction, Angle);
        }

        #endregion
    }
}