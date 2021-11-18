using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Controls.Interactions
{
    /// <summary>
    /// A class for grabbing objects with a grabbable component attached to them.
    /// </summary>
    public class Grabber : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>An event invoked when grabber grabs a grabbable object.</summary>
        [Tooltip("An event invoked when grabber grabs a grabbable object.")] [SerializeField]
        private UnityEvent grab;

        /// <summary>An event invoked when grabber stops grabbing the grabbable object.</summary>
        [Tooltip("An event invoked when grabber stops grabbing the grabbable object.")] [SerializeField]
        private UnityEvent stoppedGrabbing;

        #endregion

        #region Variables

        /// <summary>Threshold below which position of the grabbed object will be set instead of lerped.</summary>
        private const float LerpDistanceThreshold = 0.01f;

        /// <summary>Radius around the grabber in which objects can be grabbed.</summary>
        private const float GrabRadius = 0.1f;

        /// <summary>Multiplier for lerping position and rotation of the grabbed object.</summary>
        private const float LerpFactor = 20f;

        /// <summary>Grabbed object.</summary>
        private Grabbable _grabbedObject;

        /// <summary>Coroutine responsible for smoothly moving the gameobject to the grabbers position.</summary>
        private Coroutine _grabbingCoroutine;

        /// <summary>State of the grabber.</summary>
        private State _state = State.NotGrabbing;

        /// <summary>Initial rotation of grabbed object used for target rotation calculations.</summary>
        private Quaternion _initialGrabbedObjectRotation;

        /// <summary>Initial forward direction of grabber used for target rotation calculations.</summary>
        private Vector3 _initialGrabberForward;

        /// <summary>Flattened forward direction used for target rotation calculations.</summary>
        private Vector3 FlattenedForward => Vector3.ProjectOnPlane(transform.forward, Vector3.up);

        /// <summary>Property for calculating target rotation for the grabbed object.</summary>
        private Quaternion TargetRotation => _initialGrabbedObjectRotation *
                                             Quaternion.FromToRotation(_initialGrabberForward, FlattenedForward);

        /// <summary>Property for calculating target position for the grabbed object.</summary>
        private Vector3 TargetPosition =>
            transform.position + _grabbedObject.toMove.position - _grabbedObject.transform.position;

        #endregion

        #region Nested Types

        /// <summary>
        /// Enum representing the state of the grabber.
        /// </summary>
        /// <remarks>
        /// Grabbing - a grabbed object is smoothly moving to the grabbers position.<br/>
        /// Grabbed - an object is grabbed and is following the grabbers position.<br/>
        /// NotGrabbing - no object is currently grabbed.
        /// </remarks>
        private enum State
        {
            Grabbing,
            Grabbed,
            NotGrabbing
        }

        #endregion

        #region Built-in Methods

        /// <summary>
        /// If an object is grabbed, updates its position and optionally rotation to follow the grabber.
        /// </summary>
        private void Update()
        {
            if (_state == State.Grabbed)
            {
                _grabbedObject.toMove.position = TargetPosition;
                if (_grabbedObject.rotate)
                    _grabbedObject.toMove.rotation = TargetRotation;
            }
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Handler for the grab event invoked by the input handler.
        /// </summary>
        public void OnGrab()
        {
            // generate a list of colliders within range, sorted by the distance to the grabber
            var origin = transform.position;
            var colliders = Physics.OverlapSphere(origin, GrabRadius);

            // get all grabbable objects which ware attached to the same gameobject as collider and can be grabbed
            var grabbables = colliders.Select(c => c.gameObject.GetComponent<Grabbable>())
                .Where(grabbable => grabbable != null && grabbable.CanBeGrabbed).ToList();

            if (!grabbables.Any()) return; // no grabbable objects to grab

            // get the first grabbable object after order grabbables by position
            var closestGrabbable = grabbables.OrderBy(c => (origin - c.toMove.position).sqrMagnitude).First();

            // start grabbing the object
            _grabbedObject = closestGrabbable;

            // if grabbed object should be rotated save grabbed object's rotation and grabber direction
            if (_grabbedObject.rotate)
            {
                _initialGrabbedObjectRotation = _grabbedObject.toMove.rotation;
                // get flattened forward direction
                _initialGrabberForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up);
            }

            _grabbedObject.Grab(StopGrabbing);
            _state = State.Grabbing;
            grab.Invoke(); // invoke listeners on grab event
            _grabbingCoroutine = StartCoroutine(Grab());
        }

        /// <summary>
        /// Handler for the release event invoked by the input handler.
        /// </summary>
        public void OnRelease()
        {
            // if the method was run when the grabber wasn't grabbing, don't do anything
            if (_state == State.NotGrabbing) return;

            // release the grabbed object
            _grabbedObject.Release();

            StopGrabbing();
        }

        /// <summary>
        /// Stops grabbing an object.
        /// </summary>
        /// <remarks>Releasing of the grabbed object must be handled separately.</remarks>
        private void StopGrabbing()
        {
            // if currently in the process of grabbing, stop the grabbing coroutine
            if (_state == State.Grabbing)
                StopCoroutine(_grabbingCoroutine);

            _grabbedObject = null;
            _state = State.NotGrabbing;
            
            stoppedGrabbing.Invoke();
        }

        /// <summary>
        /// A coroutine for smoothly grabbing an object.
        /// </summary>
        /// <returns>IEnumerator required for the coroutine.</returns>
        private IEnumerator Grab()
        {
            // lerp the connectors' positions between the current position and grabber position until the distance is
            // above the distance threshold
            while (Vector3.Distance(_grabbedObject.toMove.position, TargetPosition) > LerpDistanceThreshold)
            {
                // move grabbed object
                _grabbedObject.toMove.position = Vector3.Lerp(_grabbedObject.toMove.position, TargetPosition,
                    LerpFactor * Time.deltaTime);

                // if rotate flag is set, rotate grabbed object
                if (_grabbedObject.rotate)
                    _grabbedObject.toMove.rotation = TargetRotation;
                yield return null;
            }

            _state = State.Grabbed;
            _grabbingCoroutine = null;
        }

        #endregion
    }
}