using UnityEngine;

namespace UI.List
{
    /// <summary>
    /// Interface for an element of a <see cref="UIList"/> with <see cref="Object"/> data.
    /// </summary>
    public interface IUIListElement
    {
        #region Custom Methods

        /// <summary>
        /// Populates the fields of a list element.
        /// </summary>
        /// <param name="listElementData"><see cref="Object"/> with data to fill the element.</param>
        void Populate(Object listElementData);

        #endregion
    }
}