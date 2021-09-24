using UnityEngine;

namespace VisualCoding.Blocks
{
    /// <summary>
    /// Class representing a block of code.
    /// </summary>
    public class Block : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] [Tooltip("A block that is connected to this block as a next one.")]
        private Block next;

        #endregion

        #region Variables

        public Block Next
        {
            get => next;
            set => next = value;
        } // a block that is connected to this block as a next one

        #endregion

        #region Custom Methods

        /// <summary>
        /// Returns a block of code that should be run next.
        /// </summary>
        /// <returns>Next <c>Block</c> that should be executed after this <c>Block</c>.</returns>
        public virtual Block NextBlock()
        {
            return Next;
        }

        #endregion
    }
}