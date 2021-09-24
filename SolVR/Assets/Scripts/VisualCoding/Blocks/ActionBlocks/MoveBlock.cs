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

        [SerializeField] [Tooltip("Number of seconds move command is executed.")]
        private float time;

        [SerializeField] [Tooltip("The torque to the move command.")]
        private float torque;

        #endregion

        #region Variables

        public float Time
        {
            get => time;
            set => time = value;
        } // the number of seconds move command is executed

        public float Torque
        {
            get => torque;
            set => torque = value;
        } // the torque to the move command

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