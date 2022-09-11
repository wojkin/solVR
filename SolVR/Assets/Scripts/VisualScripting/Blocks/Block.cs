using UnityEngine;

namespace VisualScripting.Blocks
{
    /// <summary>
    /// Class representing a block of code.
    /// </summary>
    public class Block : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>A block that is connected to this block as a next one.</summary>
        [SerializeField] [Tooltip("A block that is connected to this block as a next one.")]
        private Block next;

        #endregion

        #region Variables

        /// <summary><inheritdoc cref="next"/></summary>
        public Block Next
        {
            get => next;
            set => next = value;
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Returns a block of code that should be run next.
        /// </summary>
        /// <returns>Next block that should be executed after this <see cref="Block"/>.</returns>
        public virtual Block NextBlock()
        {
            return Next;
        }

        #endregion
    }
}