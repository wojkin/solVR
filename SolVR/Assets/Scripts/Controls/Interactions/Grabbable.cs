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

        /// <summary>Flag representing whether the object should follow <see cref="Grabber"/> rotation on Y axis.</summary>
        [SerializeField] public bool rotate;

        #endregion

        #region Variables

        /// <summary>Delegate for the on grabbable destroyed event handler.</summary>
        public delegate void OnGrabbableDestroyedHandler();

        /// <summary>Event raised when the object is destroyed.</summary>
        private event OnGrabbableDestroyedHandler OnGrabbableDestroyed;

        /// <summary>Flag showing whether the object is currently grabbed.</summary>
        private bool _isGrabbed;

        /// <summary>Property for checking whether the object can be grabbed.</summary>
        public virtual bool CanBeGrabbed => !_isGrabbed;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Invokes the <see cref="OnGrabbableDestroyed"/> event.
        /// </summary>
        protected virtual void OnDestroy()
        {
            OnGrabbableDestroyed?.Invoke();
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Method called when the object is grabbed.
        /// </summary>
        /// <remarks>
        /// Should be overriden for any extra grab functionality.
        /// </remarks>
        /// <param name="destroyedHandler">Handler for the <see cref="OnGrabbableDestroyed"/> event.</param>
        public virtual void Grab(OnGrabbableDestroyedHandler destroyedHandler)
        {
            OnGrabbableDestroyed += destroyedHandler;
            _isGrabbed = true;
        }

        /// <summary>
        /// Method called when the object is released.
        /// </summary>
        /// <remarks>
        /// Should be overriden for any extra release functionality.
        /// </remarks>
        public virtual void Release()
        {
            // remove all listeners from the on destroyed event
            foreach (var d in OnGrabbableDestroyed.GetInvocationList())
                OnGrabbableDestroyed -= (OnGrabbableDestroyedHandler) d;

            _isGrabbed = false;
        }

        #endregion
    }
}