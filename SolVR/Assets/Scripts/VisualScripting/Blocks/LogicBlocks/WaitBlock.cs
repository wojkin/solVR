using UnityEngine;
using Utils.ValueInRange;

namespace VisualScripting.Blocks.LogicBlocks
{
    public class WaitBlock : Block
    {
        /// <summary>Number of seconds move command is executed.</summary>
        [SerializeField] [Tooltip("Number of seconds an execution thread should wait for.")]
        private ValueInRange<float> time;
        
        
        /// <summary><inheritdoc cref="time"/></summary>
        public float Time
        {
            get => time.Value;
            set => time.Value = value;
        }
    }
}