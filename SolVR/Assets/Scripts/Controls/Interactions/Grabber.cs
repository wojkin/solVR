using System.Collections;
using System.Linq;
using UnityEngine;

namespace Controls.Interactions
{
    /// <summary>
    /// A class for grabbing objects with a grabbable component attached to them.
    /// </summary>
    public class Grabber : MonoBehaviour
    {
        #region Variables

        /// <summary>threshold below which position of the grabbed object will be set instead of lerped</summary>
        private const float LerpDistanceThreshold = 0.01f;

        /// <summary>the radius around the grabber in which objects can be grabbed</summary>
        private const float GrabRadius = 0.1f;

        /// <summary>multiplier for lerping position and rotation of the grabbed object</summary>
        private const float LerpFactor = 15f;

        /// <summary>the grabbed object</summary>
        private Grabbable _grabbedObject;

        /// <summary>a coroutine responsible for smoothly moving the gameobject to the grabbers position</summary>
        private Coroutine _grabbingCoroutine;

        /// <summary>state of the grabber</summary>
        private State _state = State.NotGrabbing;

        #endregion

        #region Nested Types

        /// <summary>
        /// Enum representing the state of the grabber.
        /// Grabbing - a grabbed object is smoothly moving to the grabbers position.
        /// Grabbed - an object is grabbed and is following the grabbers position.
        /// NotGrabbing - no object is currently grabbed.
        /// </summary>
        private enum State
        {
            Grabbing,
            Grabbed,
            NotGrabbing
        }

        #endregion

        #region Built-in Methods

        /// <summary>
        /// If an object is grabbed, updates its position to follow the grabber.
        /// </summary>
        private void Update()
        {
            if (_state == State.Grabbed)
                _grabbedObject.toMove.position = transform.position;
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Handler for the grab event invoked by the input handler.
        /// Finds all colliders within a range of the grabber and select not grabbed grabbables that are attached to the
        /// same game objects. Sorts all grabbables in ascending order based on the distance from the grabber. Finds the
        /// first grabbable and calls the grab method of it, starts a coroutine for smoothly grabbing it and sets the
        /// state to grabbing.
        /// </summary>
        public void OnGrab()
        {
            // generate a list of colliders within range, sorted by the distance to the grabber
            var origin = transform.position;
            var colliders = Physics.OverlapSphere(origin, GrabRadius);

            // get all grabbable objects which ware attached to the same gameobject as collider and are not grabbed
            var grabbables = colliders.Select(c => c.gameObject.GetComponent<Grabbable>())
                .Where(grabbable => grabbable != null && !grabbable.IsGrabbed).ToList();

            if (!grabbables.Any()) return; // no grabbable objects to grab

            // get the first grabbable object after order grabbables by position
            var closestGrabbable = grabbables.OrderBy(c => (origin - c.toMove.position).sqrMagnitude).First();

            _grabbedObject = closestGrabbable;
            closestGrabbable.Grab();
            _grabbingCoroutine = StartCoroutine(Grab());
            _state = State.Grabbing;
        }

        /// <summary>
        /// Handler for the release event invoked by the input handler.
        /// If the grabber is in the grabbing state, the grabbing coroutine is stopped. If it is in the grabbing or
        /// grabbed state the release method of the grabbed object is called, the grabbed object is set to null and the
        /// state is changed to not grabbing.
        /// </summary>
        public void OnRelease()
        {
            if (_state != State.NotGrabbing)
            {
                if (_state == State.Grabbing)
                    StopCoroutine(_grabbingCoroutine); // stop the grabbing coroutine

                // release the grabbed object
                _grabbedObject.Release();
                _grabbedObject = null;
                _state = State.NotGrabbing;
            }
        }

        /// <summary>
        /// A coroutine for smoothly grabbing an object.
        /// Lerps the connectors position between the current position and grabber position until the distance is above
        /// the distance threshold. When finished, sets the state to grabbed and the grabbing coroutine to null.
        /// </summary>
        /// <returns>IEnumerator required for the coroutine.</returns>
        private IEnumerator Grab()
        {
            while (Vector3.Distance(_grabbedObject.toMove.position, transform.position) > LerpDistanceThreshold)
            {
                _grabbedObject.toMove.position = Vector3.Lerp(_grabbedObject.toMove.position, transform.position,
                    LerpFactor * Time.deltaTime);
                yield return null;
            }

            _state = State.Grabbed;
            _grabbingCoroutine = null;
        }

        #endregion
    }
}