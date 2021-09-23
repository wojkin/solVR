using UnityEngine;

namespace VisualCoding.Blocks.LogicBlocks.Loop
{
    /// <summary>
    /// Class representing a block that is an implementation of the loop.
    /// </summary>
    public abstract class LoopBlock : Block
    {
        [SerializeField][Tooltip("The block that is the loop end corresponding to this loop.")]
        private LoopEndBlock endBlock;

        public LoopEndBlock EndBlock
        {
            protected get { return endBlock; }
            set { endBlock = value; }
        } // the end of the loop block
    }
}