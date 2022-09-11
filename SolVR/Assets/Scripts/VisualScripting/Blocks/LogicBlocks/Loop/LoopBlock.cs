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
        private LoopEndBlock loopEnd;

        #endregion

        #region Variables

        /// <summary><inheritdoc cref="loopEnd"/></summary>
        public LoopEndBlock LoopEnd
        {
            protected get { return loopEnd; }
            set { loopEnd = value; }
        }

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Destroy this loop's <see cref="loopEnd"/>.
        /// </summary>
        private void OnDestroy()
        {
            Destroy(loopEnd.gameObject);
        }

        #endregion
    }
}