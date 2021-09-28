using UnityEngine;

namespace VisualCoding.Blocks.LogicBlocks.Loop
{
    /// <summary>
    /// Class representing a block that is a for loop.
    /// </summary>
    public class ForBlock : LoopBlock
    {
        #region Serialized Fields

        /// <summary>Number of calls NextBlock that will be perform before it returns a block after loop end.</summary>
        [SerializeField] [Tooltip("Number of loops that will be perform.")]
        private int numberOfLoops;

        #endregion

        #region Variables

        /// <summary><inheritdoc cref="numberOfLoops"/></summary>
        public int NumberOfLoops
        {
            get => numberOfLoops;
            set => numberOfLoops = value;
        }

        /// <summary>An iteration flag shows how many times function NextBlock was called from last reset.</summary>
        public int Iteration { get; private set; }

        #endregion

        #region Custom Methods

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
        /// <returns><inheritdoc /> <see cref="Block"/> is determine by checking a number of iterations.</returns>
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

        #endregion
    }
}