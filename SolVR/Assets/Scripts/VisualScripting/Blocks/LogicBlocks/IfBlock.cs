using UnityEngine;
using VisualScripting.Values.BooleanValues;

namespace VisualScripting.Blocks.LogicBlocks
{
    /// <summary>
    /// Class representing block that determines next block based on condition.
    /// </summary>
    public class IfBlock : Block
    {
        #region Serialized Fields

        /// <summary>Block that will be next if condition is not met.</summary>
        [SerializeField] [Tooltip("Block that will be next if condition is not met.")]
        private Block @else;

        /// <summary>Condition that boolean value is checked to determine the next block.</summary>
        [SerializeField] [Tooltip("Condition that is checked to determine the next block.")]
        private BooleanValue condition;

        #endregion

        #region Variables

        /// <summary><inheritdoc cref="@else"/></summary>
        public Block Else
        {
            private get { return @else; }
            set { @else = value; }
        }

        /// <summary><inheritdoc cref="condition"/></summary>
        public BooleanValue Condition
        {
            get => condition;
            set => condition = value;
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Determines and returns the next block by checking a condition.
        /// </summary>
        /// <returns><inheritdoc /> <see cref="Block"/> is determine by checking the condition.</returns>
        public override Block NextBlock()
        {
            return Condition.GetValue() ? Next : Else;
        }

        #endregion
    }
}