using UnityEngine;

namespace Controls.Interactions
{
    /// <summary>
    /// A class representing a grabbable object. Must have a collider on the same gameobject in order to be grabbed.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class Grabbable : MonoBehaviour
    {
        public bool IsGrabbed { get; private set; } // flag showing whether the object is currently grabbed

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
    }
}