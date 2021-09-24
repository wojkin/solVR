using UnityEngine;

namespace VisualCoding.Blocks.LogicBlocks.Loop
{
    /// <summary>
    /// Class representing a block that is an end of the loop.
    /// </summary>
    public class LoopEndBlock : Block
    {
        [SerializeField][Tooltip("The block that is the loop corresponding to this loop end.")]
        private LoopBlock loop;

        public LoopBlock Loop
        {
            private get { return loop; }
            set { loop = value; }
        } // the loop block

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