using UnityEngine;

namespace VisualCoding.Blocks.UI
{
    /// <summary>
    /// A class representing an in-connector of a block.
    /// </summary>
    public class InConnector : MonoBehaviour
    {
        [SerializeField] private Block block; // the block to which the in-connector belongs

        public Block Block => block; // property for accessing the block
    }
}