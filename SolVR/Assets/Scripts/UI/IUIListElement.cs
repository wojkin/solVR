
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Interface for element in the UI list with ScriptableObject data.
    /// </summary>
    public interface IUIListElement
    {
        /// <summary>
        /// Populates the fields of a list element.
        /// Sets fields of the UI element based on provided ScriptableObject.
        /// </summary>
        /// <param name="listElementData">ScriptableObject with data to fill UI list element.</param>
        void Populate(ScriptableObject listElementData);
        
    }
}
