using UnityEngine;

namespace VisualScripting.Blocks.LogicBlocks.Loop
{
    /// <summary>
    /// Class representing a block that is an implementation of the loop.
    /// </summary>
    public abstract class LoopBlock : Block
    {
        #region Serialized Fields

        /// <summary>The block that is the loop end corresponding to this loop.</summary>
        [SerializeField] [Tooltip("The block that is the loop end corresponding to this loop.")]
        private LoopEndBlock endBlock;

        #endregion

        #region Variables

        /// <summary><inheritdoc cref="endBlock"/></summary>
        public LoopEndBlock EndBlock
        {
            protected get { return endBlock; }
            set { endBlock = value; }
        }

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Destroy this loop's <see cref="endBlock"/>.
        /// </summary>
        private void OnDestroy()
        {
            Destroy(endBlock.gameObject);
        }

        #endregion
    }
}