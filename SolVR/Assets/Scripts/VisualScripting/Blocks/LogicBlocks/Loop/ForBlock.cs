using UnityEngine;
using Utils.ValueInRange;
using VisualScripting.Blocks.Properties;

namespace VisualScripting.Blocks.LogicBlocks.Loop
{
    /// <summary>
    /// Class representing a for loop block.
    /// </summary>
    public class ForBlock : LoopBlock, IResettable
    {
        #region Serialized Fields

        /// <summary>Number of times the blocks inside the loop will be executed.</summary>
        [SerializeField] [Tooltip("Number of loops that will be performed.")]
        private ValueInRange<int> numberOfLoops;

        #endregion

        #region Variables

        /// <summary>Delegate for <see cref="ForBlock.IterationChanged"/> event.</summary>
        public delegate void IterationChangedHandler(int iteration);

        /// <summary>Event invoked when the current count of iterations changes.</summary>
        public event IterationChangedHandler IterationChanged;

        /// <summary><inheritdoc cref="numberOfLoops"/></summary>
        public int NumberOfLoops
        {
            get => numberOfLoops.Value;
            set => numberOfLoops.Value = value;
        }

        /// <summary>An iteration counter showing how many iterations of the loop were completed. If the loop is not
        /// running the iteration count is zero.</summary>
        private int _iteration;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initialize fields.
        /// </summary>
        private void Start()
        {
            numberOfLoops.Initialize();
        }

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
            if (_iteration >= NumberOfLoops) return LoopEnd.Next;
            _iteration++;
            IterationChanged?.Invoke(_iteration);
            return Next;

        }

        #endregion

        #region IResettable Methods

        /// <summary>
        /// Resets <see cref="_iteration"/> counter.
        /// </summary>
        public void Reset()
        {
            _iteration = 0;
            IterationChanged?.Invoke(_iteration);
        }

        #endregion
    }
}