using Robots.Commands;
using UnityEngine;
using Utils.ValueInRange;

namespace VisualCoding.Blocks.ActionBlocks
{
    /// <summary>
    /// Class representing a block and creating a move command.
    /// </summary>
    public class MoveBlock : Block, IGetCommand
    {
        #region Serialized Fields

        /// <summary>Number of seconds move command is executed.</summary>
        [SerializeField] [Tooltip("Number of seconds move command is executed.")]
        private ValueInRange<float> time;

        /// <summary>The torque to the move command.</summary>
        [SerializeField] [Tooltip("The torque to the move command.")]
        private ValueInRange<float> torque;

        #endregion

        #region Variables

        /// <summary><inheritdoc cref="time"/></summary>
        public float Time
        {
            get => time.Value;
            set => time.Value = value;
        }

        /// <summary><inheritdoc cref="torque"/></summary>
        public float Torque
        {
            get => torque.Value;
            set => torque.Value = value;
        }

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initialize fields.
        /// </summary>
        private void Start()
        {
            time.Initialize();
            torque.Initialize();
        }

        #endregion

        #region IGetCommand Methods

        /// <summary>
        /// Creates and gets a move command.
        /// </summary>
        /// <returns>A created MoveCommand with time and torque given as constructor parameters.</returns>
        public ICommand GetCommand()
        {
            return new MoveCommand(Time, Torque);
        }

        #endregion
    }
}