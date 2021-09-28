using Robots.Commands;
using UnityEngine;

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
        private float time;

        /// <summary>The torque to the move command.</summary>
        [SerializeField] [Tooltip("The torque to the move command.")]
        private float torque;

        #endregion

        #region Variables

        /// <summary><inheritdoc cref="time"/></summary>
        public float Time
        {
            get => time;
            set => time = value;
        }

        /// <summary><inheritdoc cref="torque"/></summary>
        public float Torque
        {
            get => torque;
            set => torque = value;
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