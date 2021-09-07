namespace VisualCoding.Blocks.LogicBlocks.Loop
{
    /// <summary>
    /// Class representing a block that is a for loop.
    /// </summary>
    public class ForBlock : LoopBlock
    {
        // number of calls NextBlock that will be perform before it returns a block after loop end
        public int NumberOfLoops { get; set; }

        // iteration flag that shows how many times function NextBlock was called from last reset
        public int Iteration { get; private set; }

        /// <summary>
        /// Initializing fields.
        /// </summary>
        private ForBlock()
        {
            Iteration = 0;
        }

        /// <summary>
        /// Determines the next block by checking an iteration flag if it is under the number of loops in the loop.
        /// Increases the iteration flag by one and resets it if it reaches the number of loops.
        /// </summary>
        /// <returns><inheritdoc /> <c>Block</c> is determine by checking a number of iterations.</returns>
        public override Block NextBlock()
        {
            Iteration++;
            if (Iteration <= NumberOfLoops)
            {
                return Next;
            }
            else
            {
                Iteration = 0;
                return EndBlock.Next;
            }
        }
    }
}