using UnityEngine;

namespace VisualCoding.Blocks.LogicBlocks.Loop
{
    /// <summary>
    /// Class representing a for loop block.
    /// </summary>
    public class ForBlock : LoopBlock
    {
        #region Serialized Fields

        /// <summary>Number of times the blocks inside the loop will be executed.</summary>
        [SerializeField] [Tooltip("Number of loops that will be performed.")]
        private int numberOfLoops;

        #endregion

        #region Variables

        /// <summary>Delegate for <see cref="ForBlock.IterationChanged"/> event.</summary>
        public delegate void IterationChangedHandler(int iteration);

        /// <summary>Event invoked when the current count of iterations changes.</summary>
        public event IterationChangedHandler IterationChanged;

        /// <summary><inheritdoc cref="numberOfLoops"/></summary>
        public int NumberOfLoops
        {
            get => numberOfLoops;
            set => numberOfLoops = value;
        }

        /// <summary>An iteration counter showing how many iterations of the loop were completed. If the loop is not
        /// running the iteration count is zero.</summary>
        private int Iteration { get; set; }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Determines the next block by checking if the iteration counter is below the target number of loops.
        /// Increases the iteration counter by one and resets it if it reaches the target number of loops.
        /// </summary>
        /// <returns><inheritdoc /> <see cref="Block"/> is determined by checking if the target number of iterations has
        /// been reached.</returns>
        public override Block NextBlock()
        {
            Block nextBlock;

            if (Iteration < NumberOfLoops)
            {
                Iteration++;
                nextBlock = Next;
            }
            else
            {
                Iteration = 0;
                nextBlock = EndBlock.Next;
            }

            IterationChanged?.Invoke(Iteration);
            return nextBlock;
        }

        #endregion
    }
}