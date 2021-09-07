using VisualCoding.ConditionExpression.Conditions;

namespace VisualCoding.Blocks.LogicBlocks.Loop
{
    /// <summary>
    /// Class representing a block that is a while loop.
    /// </summary>
    public class WhileBlock : LoopBlock
    {
        public Condition Condition { get; set; } // condition that is checked to determine the next block

        /// <summary>
        /// Determines the next block by checking a condition.
        /// Returns the next block if the condition is met or returns the loop end block if it's not.
        /// </summary>
        /// <returns><inheritdoc /> <c>Block</c> is determine by checking the condition.</returns>
        public override Block NextBlock()
        {
            return  Condition.Check() ? Next : EndBlock.Next;
        }
    }
}