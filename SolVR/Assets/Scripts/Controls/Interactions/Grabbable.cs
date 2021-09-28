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

        /// <summary>the transform that will be changed to move the object</summary>
        [SerializeField] public Transform toMove;

        #endregion

        #region Variables

        /// <summary>flag showing whether the object is currently grabbed</summary>
        public bool IsGrabbed { get; private set; }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Function, which should be called when the object is grabbed.
        /// Sets the grabbed flag to true. Should be overriden for any extra grab functionality.
        /// </summary>
        public virtual void Grab()
        {
            IsGrabbed = true;
        }

        /// <summary>
        /// Function, which should be called when the object is released.
        /// Sets the grabbed flag to false. Should be overriden for any extra release functionality.
        /// </summary>
        public virtual void Release()
        {
            IsGrabbed = false;
        }

        #endregion
    }
}