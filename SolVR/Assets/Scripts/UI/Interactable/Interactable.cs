using UnityEngine;

namespace UI.Interactable
{
    /// <summary>
    /// An abstract class representing a interactable object.
    /// </summary>
    public abstract class Interactable : MonoBehaviour
    {
        #region Custom Methods

        /// <summary>
        /// Sets object intractability based on <see cref="value"/>
        /// </summary>
        /// <param name="value">Boolean value that specifies if object will be set interactable.</param>
        public abstract void SetInteractable(bool value);

        #endregion
    }
}