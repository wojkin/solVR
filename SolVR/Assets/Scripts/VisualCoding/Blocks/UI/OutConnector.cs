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

        /// <summary>Event for configuring which property will be set by this connector.</summary> 
        [SerializeField] private UnityEvent<Block> connectedBlock;

        /// <summary>Origin of the connector.</summary>
        [SerializeField] private Transform origin;

        #endregion

        #region Variables

        /// <summary>Radius within which objects can be grabbed.</summary>
        private const float ConnectRadius = 0.1f;

        /// <summary>Multiplier for lerping position and rotation of the grabbed object.</summary>
        private const float LerpFactor = 8f;

        /// <summary>Distance below which the objects position should be changed directly instead of lerped.</summary>
        private const float LerpDistanceThreshold = 0.01f;

        /// <summary>Angle below which the objects rotation should be changed directly instead of lerped.</summary>
        private const float LerpAngleThreshold = 1f;

        /// <summary>Line renderer responsible for visualizing the connection.</summary>
        private LineRenderer _connectionVisual;

        /// <summary>Transform which the connector should follow.</summary>
        private Transform _targetTransform;

        /// <summary>State the connector is in.</summary>
        private State _state;

        #endregion

        #region Nested Types

        /// <summary>
        /// Enum representing states the connector can be in.
        /// </summary>
        /// <remarks>
        /// Resting - the connector is at it's origin.<br/>
        /// Connected - the connector is connected to anthers block in-connector.<br/>
        /// Disconnected - the connector is neither Resting or Connected.
        /// </remarks>
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
        /// </summary>
        private void Update()
        {
            // if the connector is not resting (either connected or disconnected), updated it's line connection positions
            if (_state != State.Resting)
            {
                SetConnectionPositions();
                // if it's connected, update it's position to follow the in-connector it's connected to
                if (_state == State.Connected)
                    FollowTarget();
            }
        }

        #endregion

        #region Custom Methods
        
        /// <summary>
        /// This method handles grabbing of the connector.
        /// </summary>
        public override void Grab()
        {
            if (_state == State.Connected)
                connectedBlock.Invoke(null); // if the connector was connected, set the connected block to null
            else
                _connectionVisual.enabled = true; // If it wasn't connected, enable the connection visual

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
        /// Searches for the closest in-connector in range and returns it.
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
        /// </summary>
        /// <returns>IEnumerator required for the coroutine.</returns>
        private IEnumerator ReachTarget()
        {
            // lerps the connectors position and rotation between their current and target values until both the
            // distance and angle to the target are below thresholds
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