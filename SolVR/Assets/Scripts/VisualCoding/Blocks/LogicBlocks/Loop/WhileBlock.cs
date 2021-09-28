using UnityEngine;
using VisualCoding.Values.BooleanValues;

namespace VisualCoding.Blocks.LogicBlocks.Loop
{
    /// <summary>
    /// Class representing a block that is a while loop.
    /// </summary>
    public class WhileBlock : LoopBlock
    {
        #region Serialized Fields

        /// <summary>Condition that boolean value is checked to determine the next block.</summary>
        [SerializeField] [Tooltip("Condition that is checked to determine the next block.")]
        private BooleanValue condition;

        #endregion

        #region Variables

        /// <summary><inheritdoc cref="condition"/></summary>
        public BooleanValue Condition
        {
            get => condition;
            set => condition = value;
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Determines the next block by checking a condition.
        /// Returns the next block if the condition is met or returns the loop end block if it's not.
        /// </summary>
        /// <returns><inheritdoc /> <see cref="Block"/> is determine by checking the condition.</returns>
        public override Block NextBlock()
        {
            return Condition.GetValue() ? Next : EndBlock.Next;
        }

        #endregion
    }
}