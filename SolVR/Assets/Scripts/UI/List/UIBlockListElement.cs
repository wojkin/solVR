using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace UI.List
{
    /// <summary>
    /// Class representing an element of a <see cref="UIList"/> displaying block names.
    /// </summary>
    public class UIBlockListElement : MonoBehaviour, IUIListElement
    {
        #region Serialized Fields

        /// <summary>Text for displaying the block name.</summary>
        [SerializeField] private TextMeshProUGUI text;

        #endregion

        #region IUIListElement Methods

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="listElementData"><see cref="BlockData"/> used to populate the list element.</param>
        public void Populate(Object listElementData)
        {
            var blockData = (BlockData) listElementData;
            text.text = blockData.BlockName;
        }

        #endregion
    }
}