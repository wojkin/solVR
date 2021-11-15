using Robots.Commands;
using Robots.Enums;
using UnityEngine;
using Utils.ValueInRange;

namespace VisualCoding.Blocks.ActionBlocks
{
    /// <summary>
    /// Class representing a turn block which creates a turn command.
    /// </summary>
    public class TurnBlock : Block, IGetCommand
    {
        #region Serialized Fields

        /// <summary>The direction in which the robot should turn.</summary>
        [SerializeField] [Tooltip("The direction in which the robot should turn.")]
        private TurnDirection direction;

        /// <summary>The steer angle of the wheels around the local vertical axis in range.</summary>
        [SerializeField] [Tooltip("The steer angle of the wheels around the local vertical axis in range.")]
        public ValueInRange<int> angle;

        #endregion

        #region Variables

        /// <summary><inheritdoc cref="direction"/></summary>
        public TurnDirection Direction
        {
            get => direction;
            set => direction = value;
        }

        /// <summary><inheritdoc cref="angle"/></summary>
        public int Angle
        {
            get => angle.Value;
            set => angle.Value = value;
        }

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initialize fields.
        /// </summary>
        public void Start()
        {
            angle.Initialize();
        }

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