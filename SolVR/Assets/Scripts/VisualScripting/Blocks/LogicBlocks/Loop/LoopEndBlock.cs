using UnityEngine;

namespace VisualScripting.Blocks.LogicBlocks.Loop
{
    /// <summary>
    /// Class representing a block that is an end of the loop.
    /// </summary>
    public class LoopEndBlock : Block
    {
        #region Serialized Fields

        /// <summary>The block that is the loop corresponding to this loop end.</summary>
        [SerializeField] [Tooltip("The block that is the loop corresponding to this loop end.")]
        private LoopBlock loop;

        #endregion

        #region Variables

        /// <summary><inheritdoc cref="loop"/></summary>
        public LoopBlock Loop
        {
            private get { return loop; }
            set { loop = value; }
        }

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