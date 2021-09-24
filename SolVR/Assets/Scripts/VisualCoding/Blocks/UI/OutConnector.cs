using System.Collections;
using System.Linq;
using Controls.Interactions;
using UnityEngine;
using UnityEngine.Events;

namespace VisualCoding.Blocks.UI
{
    /// <summary>
    /// A class representing the out-connector of a block, responsible for connecting and visualizing the connection.
    /// </summary>
    [RequireComponent(typeof(LineRenderer))]
    public class OutConnector : Grabbable
    {
        #region Serialized Fields

        // event for configuring which property will be set by this connector
        [SerializeField] private UnityEvent<Block> connectedBlock;
        [SerializeField] private Transform origin; // the origin of the connector

        #endregion

        #region Variables

        private const float ConnectRadius = 0.1f; // the radius within which objects can be grabbed

        private const float LerpFactor = 8f; // multiplier for lerping position and rotation of the grabbed object

        // distance below which the objects position should be changed directly instead of lerped
        private const float LerpDistanceThreshold = 0.01f;

        // angle below which the objects rotation should be changed directly instead of lerped
        private const float LerpAngleThreshold = 1f;

        private LineRenderer _connectionVisual; // line renderer responsible for visualizing the connection
        private Transform _targetTransform; // the transform which the connector should follow
        private State _state; // state the connector is in

        #endregion

        #region Nested Types

        /// <summary>
        /// Enum representing states the connector can be in.
        /// Resting - the connector is at it's origin.
        /// Connected - the connector is connected to anthers block in-connector.
        /// Disconnected - the connector is neither Resting or Connected.
        /// </summary>
        private enum State
        {
            Resting,
            Connected,
            Disconnected
        }

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initializes the connection visual.
        /// </summary>
        private void Start()
        {
            _connectionVisual = GetComponent<LineRenderer>();
            _connectionVisual.enabled = false;
        }

        /// <summary>
        /// Sets line connection positions and object position if needed.
        /// If the connector is not resting (either connected or disconnected), its line connection positions are
        /// updated. If it's connected, its position is updated to follow the in-connector its connected to.
        /// </summary>
        private void Update()
        {
            if (_state != State.Resting)
            {
                SetConnectionPositions();
                if (_state == State.Connected)
                    FollowTarget();
            }
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Sets the line visual positions so the it connects the origin with the out-connector.
        /// </summary>
        private void SetConnectionPositions()
        {
            _connectionVisual.SetPosition(0, origin.position);
            _connectionVisual.SetPosition(1, transform.position);
        }

        /// <summary>
        /// Updates the connector position and rotation so it follows its target.
        /// </summary>
        private void FollowTarget()
        {
            transform.position = _targetTransform.position;
            transform.rotation = _targetTransform.rotation;
        }

        /// <summary>
        /// This method handles grabbing of the connector.
        /// If the connector was connected, the connected block is set to null. If it wasn't connected (it was
        /// Resting), the connection visual is enabled. The state is changed to Disconnected and the base method is
        /// called.
        /// </summary>
        public override void Grab()
        {
            if (_state == State.Connected)
                connectedBlock.Invoke(null);
            else
                _connectionVisual.enabled = true;

            _state = State.Disconnected;
            base.Grab();
        }

        /// <summary>
        /// This method handles releasing of the connector.
        /// Searches for the nearest in-connector in range. If it was found, starts the connecting coroutine, otherwise
        /// starts the wind back coroutine.
        /// </summary>
        public override void Release()
        {
            var inConnector = ClosestInConnector();

            if (inConnector != null)
                StartCoroutine(Connect(inConnector));
            else
                StartCoroutine(WindBack());
        }

        /// <summary>
        /// Searches for the closest in-connector in range and returns it.
        /// Finds all colliders within a range of the connector and sorts them in ascending order based on the distance
        /// from the connector. Finds the the first in-connector which was attached to one of the colliders gameobjects.
        /// If no in-connector was found, it returns null.
        /// </summary>
        /// <returns>The closest in-connector within range or null if no in-connector was found.</returns>
        private InConnector ClosestInConnector()
        {
            // generate a list of colliders within range, sorted by the distance to the connector
            var origin = transform.position;
            var colliders = Physics.OverlapSphere(origin, ConnectRadius);
            colliders = colliders.OrderBy(c => (origin - c.transform.position).sqrMagnitude).ToArray();

            // find the first in-connector which was attached to one of the colliders gameobjects
            foreach (var collider in colliders)
            {
                var inConnector = collider.gameObject.GetComponent<InConnector>();
                if (inConnector != null)
                    return inConnector;
            }

            return null;
        }

        /// <summary>
        /// A coroutine for smoothly returning the connector to its origin.
        /// Sets the target transform to the connectors origin. Starts the coroutine for reaching the target. When its
        /// finished it sets the state to resting, disables the connection visual and calls the base method.
        /// </summary>
        /// <returns>IEnumerator required for the coroutine.</returns>
        private IEnumerator WindBack()
        {
            _targetTransform = origin;

            yield return ReachTarget();

            _state = State.Resting;
            _connectionVisual.enabled = false;
            base.Release();
        }

        /// <summary>
        /// A coroutine for smoothly connecting the connector to an in-connector.
        /// Sets the connectors target to the in-connectors transform. Starts the coroutine for reaching the target.
        /// When its finished it sets the state to connected, sets the connected block to the in-connectors block and
        /// calls the base method.
        /// </summary>
        /// <param name="inConnector">The in-connector to which the out-connector will be connected.</param>
        /// <returns>IEnumerator required for the coroutine.</returns>
        private IEnumerator Connect(InConnector inConnector)
        {
            _targetTransform = inConnector.transform;

            yield return ReachTarget();

            _state = State.Connected;
            connectedBlock.Invoke(inConnector.Block);
            base.Release();
        }

        /// <summary>
        /// A coroutine for smoothly reaching a target.
        /// Lerps the connectors position and rotation between their current and target values until
        /// both the distance and angle to the target are below thresholds.
        /// </summary>
        /// <returns>IEnumerator required for the coroutine.</returns>
        private IEnumerator ReachTarget()
        {
            while (Vector3.Distance(transform.position, _targetTransform.position) > LerpDistanceThreshold ||
                   Quaternion.Angle(transform.rotation, _targetTransform.rotation) > LerpAngleThreshold)
            {
                transform.position = Vector3.Lerp(transform.position, _targetTransform.position,
                    LerpFactor * Time.deltaTime);

                transform.rotation = Quaternion.Lerp(transform.rotation, _targetTransform.rotation,
                    LerpFactor * Time.deltaTime);

                yield return null;
            }
        }

        #endregion
    }
}