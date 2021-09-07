namespace VisualCoding.Blocks.LogicBlocks.Loop
{
    /// <summary>
    /// Class representing a block that is an end of the loop.
    /// </summary>
    public class LoopEndBlock : Block
    {
        public LoopBlock Loop { private get; set; } // the loop block

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <returns>The loop block that should be executed after this block.</returns>
        public override Block NextBlock()
        {
            return Loop;
        }
    }
}