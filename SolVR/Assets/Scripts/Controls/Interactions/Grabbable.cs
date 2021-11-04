using UnityEngine;

namespace Controls.Interactions
{
    /// <summary>
    /// A class representing a grabbable object. Must have a collider on the same gameobject in order to be grabbed.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class Grabbable : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>The transform that will be changed to move the object.</summary>
        [SerializeField] public Transform toMove;
        [SerializeField] public bool rotate;

        #endregion

        #region Variables

        /// <summary>Flag showing whether the object is currently grabbed.</summary>
        public bool IsGrabbed { get; private set; }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Function called when the object is grabbed.
        /// </summary>
        /// <remarks>
        /// Should be overriden for any extra grab functionality.
        /// </remarks>
        public virtual void Grab()
        {
            IsGrabbed = true;
        }

        /// <summary>
        /// Function called when the object is released.
        /// </summary>
        /// <remarks>
        /// Should be overriden for any extra release functionality.
        /// </remarks>
        public virtual void Release()
        {
            IsGrabbed = false;
        }

        #endregion
    }
}