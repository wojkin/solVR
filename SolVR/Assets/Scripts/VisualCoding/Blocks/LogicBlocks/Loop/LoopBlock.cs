namespace VisualCoding.Blocks.LogicBlocks.Loop
{
    /// <summary>
    /// Class representing a block that is an implementation of the loop.
    /// </summary>
    public abstract class LoopBlock : Block
    {
        public LoopEndBlock EndBlock { protected get; set; } // the end of the loop block
    }
}