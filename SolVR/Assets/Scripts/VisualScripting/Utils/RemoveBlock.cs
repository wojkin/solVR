using UnityEngine;
using VisualScripting.Blocks;

namespace VisualScripting.Utils
{
    /// <summary>
    /// Component for removing a <see cref="Block"/> by a method call.
    /// </summary>
    public class RemoveBlock : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>Block which will be removed.</summary>
        [SerializeField] private Block blockToRemove;

        #endregion

        #region Custom Methods

        /// <summary>
        /// Destroys the <see cref="blockToRemove"/>.
        /// </summary>
        public void Remove()
        {
            Destroy(blockToRemove.gameObject);
        }

        #endregion
    }
}