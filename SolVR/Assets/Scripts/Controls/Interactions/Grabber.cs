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
        // threshold below which position of the grabbed object will be set instead of lerped
        private const float LerpDistanceThreshold = 0.01f;
        private const float GrabRadius = 0.1f; // the radius around the grabber in which objects can be grabbed
        private const float LerpFactor = 15f; // multiplier for lerping position and rotation of the grabbed object

        private Grabbable _grabbedObject; // the grabbed object

        // a coroutine responsible for smoothly moving the gameobject to the grabbers position
        private Coroutine _grabbingCoroutine;
        private State _state; // state of the grabber

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

        /// <summary>
        /// If an object is grabbed, updates its position to follow the grabber.
        /// </summary>
        private void Update()
        {
            if (_state == State.Grabbed)
                _grabbedObject.transform.position = transform.position;
        }

        /// <summary>
        /// Handler for the grab event invoked by the input handler.
        /// Finds all colliders within a range of the grabber and sorts them in ascending order based on the distance
        /// from the grabber. Finds the the first grabbable object which was attached to one of the colliders
        /// gameobjects. Calls the grab method of this object, starts a coroutine for smoothly grabbing it and sets the
        /// state to grabbing.
        /// </summary>
        public void OnGrab()
        {
            // generate a list of colliders within range, sorted by the distance to the grabber
            var origin = transform.position;
            var colliders = Physics.OverlapSphere(origin, GrabRadius);
            colliders = colliders.OrderBy(c => (origin - c.transform.position).sqrMagnitude).ToArray();

            // find the first grabbable object which was attached to one of the colliders gameobjects
            foreach (var collider in colliders)
            {
                var grabbable = collider.gameObject.GetComponent<Grabbable>();
                if (grabbable != null && !grabbable.IsGrabbed)
                {
                    _grabbedObject = grabbable;
                    grabbable.Grab();
                    _grabbingCoroutine = StartCoroutine(Grab());
                    _state = State.Grabbing;
                    break;
                }
            }
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
            while (Vector3.Distance(_grabbedObject.transform.position, transform.position) > LerpDistanceThreshold)
            {
                _grabbedObject.transform.position = Vector3.Lerp(_grabbedObject.transform.position, transform.position,
                    LerpFactor * Time.deltaTime);
                yield return null;
            }

            _state = State.Grabbed;
            _grabbingCoroutine = null;
        }
    }
}