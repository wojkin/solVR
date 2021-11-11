using UnityEngine;

namespace VisualCoding.Blocks.Connectors
{
    /// <summary>
    /// A class representing an in-connector of a block.
    /// </summary>
    public class InConnector : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>The block to which the in-connector belongs.</summary>
        [SerializeField] private Block block;

        #endregion

        #region Variables

        /// <summary>Property for accessing the block.</summary>
        public Block Block => block;

        #endregion
    }
}