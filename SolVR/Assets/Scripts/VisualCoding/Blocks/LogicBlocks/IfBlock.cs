using UnityEngine;
using VisualCoding.Values.BooleanValues;

namespace VisualCoding.Blocks.LogicBlocks
{
    /// <summary>
    /// Class representing block that determines next block based on condition.
    /// </summary>
    public class IfBlock : Block
    {
        #region Variables

        [SerializeField] [Tooltip("Block that will be next if condition is not met.")]
        private Block @else;

        [SerializeField] [Tooltip("Condition that is checked to determine the next block.")]
        private BooleanValue condition;

        public Block Else
        {
            private get { return @else; }
            set { @else = value; }
        } // block that will be returned if condition is not met

        public BooleanValue Condition
        {
            get => condition;
            set => condition = value;
        } // condition that is checked to determine the next block

        #endregion

        #region Custom methods

        /// <summary>
        /// Determines and returns the next block by checking a condition.
        /// </summary>
        /// <returns><inheritdoc /> <c>Block</c> is determine by checking the condition.</returns>
        public override Block NextBlock()
        {
            return Condition.GetValue() ? Next : Else;
        }

        #endregion
    }
}