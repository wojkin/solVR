using UnityEngine;

namespace UI
{
    /// <summary>
    /// Interface for an element of a <see cref="UIList"/> with <see cref="ScriptableObject"/> data.
    /// </summary>
    public interface IUIListElement
    {
        #region Custom Methods

        /// <summary>
        /// Populates the fields of a list element.
        /// </summary>
        /// <param name="listElementData"><see cref="ScriptableObject"/> with data to fill the element.</param>
        void Populate(ScriptableObject listElementData);

        #endregion
    }
}