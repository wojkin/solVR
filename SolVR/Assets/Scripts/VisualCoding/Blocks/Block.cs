using UnityEngine;

namespace VisualCoding.Blocks
{
    /// <summary>
    /// Class representing a block of code.
    /// </summary>
    public class Block : MonoBehaviour
    {
        [SerializeField] [Tooltip("A block that is connected to this block as a next one.")]
        private Block next;

        public Block Next
        {
            get => next;
            set => next = value;
        } // a block that is connected to this block as a next one

        /// <summary>
        /// Returns a block of code that should be run next.
        /// </summary>
        /// <returns>Next <c>Block</c> that should be executed after this <c>Block</c>.</returns>
        public virtual Block NextBlock()
        {
            return Next;
        }
    }
}