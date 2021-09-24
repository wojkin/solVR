using UnityEngine;

namespace VisualCoding.Blocks.LogicBlocks.Loop
{
    /// <summary>
    /// Class representing a block that is an end of the loop.
    /// </summary>
    public class LoopEndBlock : Block
    {
        #region Serialized Fields

        [SerializeField] [Tooltip("The block that is the loop corresponding to this loop end.")]
        private LoopBlock loop;

        #endregion

        #region Variables

        public LoopBlock Loop
        {
            private get { return loop; }
            set { loop = value; }
        } // the loop block

        #endregion

        #region Custom Methods

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        /// <returns>The loop block that should be executed after this block.</returns>
        public override Block NextBlock()
        {
            return Loop;
        }

        #endregion
    }
}