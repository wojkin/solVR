using VisualCoding.Values.BooleanValues;

namespace VisualCoding.Blocks.LogicBlocks
{
    /// <summary>
    /// Class representing block that determines next block based on condition.
    /// </summary>
    public class IfBlock : Block
    {
        public Block Else { private get; set; } // block that will be returned if condition is not met

        public BooleanValue Condition { get; set; } // condition that is checked to determine the next block
        
        /// <summary>
        /// Determines and returns the next block by checking a condition.
        /// </summary>
        /// <returns><inheritdoc /> <c>Block</c> is determine by checking the condition.</returns>
        public override Block NextBlock()
        {
            return Condition.GetValue() ?  Next : Else;
        }
    }
}