using UnityEngine;
using Utils.ValueInRange;

namespace VisualScripting.Blocks.LogicBlocks
{
    public class WaitBlock : Block
    {
        #region Serialized Fields

        /// <summary>Number of seconds move command is executed.</summary>
        [SerializeField] [Tooltip("Number of seconds an execution thread should wait for.")]
        private ValueInRange<float> time;

        #endregion

        #region Variables

        /// <summary><inheritdoc cref="time"/></summary>
        public float Time
        {
            get => time.Value;
            set => time.Value = value;
        }

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initialize fields.
        /// </summary>
        private void Start()
        {
            time.Initialize();
        }

        #endregion
    }
}