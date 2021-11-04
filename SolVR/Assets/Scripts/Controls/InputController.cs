using InputActions;
using Managers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Controls
{
    /// <summary>
    /// Class which handles input actions and invokes events based on them.
    /// </summary>
    public class InputController : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>
        /// Event invoked on TwoHandedManipulation input action started, first parameter is right hand position and
        /// the second one is left hand position.
        /// </summary>
        public UnityEvent<Vector3, Vector3> onTwoHandedManipulationStarted;

        /// <summary>
        /// Event invoked from start to cancel of TwoHandedManipulation input action, first parameter is right hand
        /// position and the second is left hand position.
        /// </summary>
        public UnityEvent<Vector3, Vector3> onTwoHandedManipulating;

        /// <summary>Unity event for handling left hand grab action.</summary>
        public UnityEvent onGrabLeftHand;

        /// <summary>Unity event for handling left hand release action.</summary>
        public UnityEvent onReleaseLeftHand;

        /// <summary>Unity event for handling right hand grab action.</summary>
        public UnityEvent onGrabRightHand;

        /// <summary>Unity event for handling right hand grab action.</summary>
        public UnityEvent onReleaseRightHand;

        /// <summary>XR Rig object transform.</summary>
        public Transform XRRigTransform;

        #endregion

        #region Variables

        /// <summary>Player input action with manipulation action.</summary>
        private PlayerInputActions _playerInputActions;

        /// <summary>XRI input action with hands positions.</summary>
        private XRIDefaultInputActions _xriInputActions;

        /// <summary>
        /// Flag showing if two handed manipulation is in progress, set to true after input action is started, until
        /// it's canceled.
        /// </summary>
        private bool _isManipulated;

        /// <summary>Flag showing whether left hand is currently grabbing.</summary>
        private bool _isGrabbingLeftHand;

        /// <summary>Flag showing whether right hand is currently grabbing.</summary>
        private bool _isGrabbingRightHand;
        
        /// <summary>Right hand position accounted for XR Rig's offset from scene center.</summary>
        private Vector3 RightHandPosition => _xriInputActions.XRIRightHand.Position.ReadValue<Vector3>() + XRRigTransform.position;
        
        /// <summary>Left hand position accounted for XR Rig's offset from scene center.</summary>
        private Vector3 LeftHandPosition => _xriInputActions.XRILeftHand.Position.ReadValue<Vector3>() + XRRigTransform.position;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initialize fields.
        /// </summary>
        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();
            _xriInputActions = new XRIDefaultInputActions();
        }

        /// <summary>
        /// Invokes onTwoHandedManipulating event, with positions of right and left hands as parameters, as long as two
        /// handed manipulation is in progress.
        /// </summary>
        private void Update()
        {
            if (_isManipulated)
                onTwoHandedManipulating?.Invoke(RightHandPosition, LeftHandPosition);
        }

        /// <summary>
        /// Enables input actions and subscribes to all needed events.
        /// </summary>
        private void OnEnable()
        {
            _xriInputActions.XRIRightHand.Position.Enable();
            _xriInputActions.XRILeftHand.Position.Enable();

            _playerInputActions.Player.TwoHandedManipulation.Enable();
            _playerInputActions.Player.TwoHandedManipulation.started += OnTwoHandedManipulationStarted;
            _playerInputActions.Player.TwoHandedManipulation.canceled += OnTwoHandedManipulationCanceled;

            _playerInputActions.Player.GrabLeftHand.Enable();
            _playerInputActions.Player.GrabLeftHand.started += OnGrabLeftHandStarted;
            _playerInputActions.Player.GrabLeftHand.canceled += OnGrabLeftHandCancelled;

            _playerInputActions.Player.GrabRightHand.Enable();
            _playerInputActions.Player.GrabRightHand.started += OnGrabRightHandStarted;
            _playerInputActions.Player.GrabRightHand.canceled += OnGrabRightHandCancelled;

            GameManager.OnPause += OnPause;
        }

        /// <summary>
        /// Disables input actions and unsubscribes from all events.
        /// </summary>
        private void OnDisable()
        {
            GameManager.OnPause -= OnPause;

            _playerInputActions.Player.TwoHandedManipulation.started -= OnTwoHandedManipulationStarted;
            _playerInputActions.Player.TwoHandedManipulation.canceled -= OnTwoHandedManipulationCanceled;

            _playerInputActions.Player.GrabLeftHand.started -= OnGrabLeftHandStarted;
            _playerInputActions.Player.GrabLeftHand.canceled -= OnGrabLeftHandCancelled;

            _playerInputActions.Player.GrabRightHand.started -= OnGrabRightHandStarted;
            _playerInputActions.Player.GrabRightHand.canceled -= OnGrabRightHandCancelled;
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Handler for after pause event.
        /// </summary>
        private void OnPause()
        {
            _isManipulated = false;

            // invoke release events if grabbing was in progress
            if (_isGrabbingLeftHand)
            {
                onReleaseLeftHand?.Invoke();
                _isGrabbingLeftHand = false;
            }

            if (_isGrabbingRightHand)
            {
                onReleaseRightHand?.Invoke();
                _isGrabbingRightHand = false;
            }
        }

        /// <summary>
        /// Handler for started event of TwoHandedManipulation input action.
        /// </summary>
        /// <param name="context">Input action event callback context, required for handling input action event.</param>
        private void OnTwoHandedManipulationStarted(InputAction.CallbackContext context)
        {
            if (GameManager.gameIsPaused)
                return;
            _isManipulated = true;
            // invoke onTwoHandedManipulationStarted event with positions of right and left hands as parameters
            onTwoHandedManipulationStarted?.Invoke(RightHandPosition, LeftHandPosition);
        }

        /// <summary>
        /// Handler for canceled event of TwoHandedManipulation input action.
        /// </summary>
        /// <param name="context">Input action event callback context, required for handling input action event.</param>
        private void OnTwoHandedManipulationCanceled(InputAction.CallbackContext context) => _isManipulated = false;

        /// <summary>
        /// Handler for the start event of the left hand grab input action.
        /// </summary>
        /// <param name="context">Input action event callback context, required for handling input action event.</param>
        private void OnGrabLeftHandStarted(InputAction.CallbackContext context)
        {
            if (GameManager.gameIsPaused)
                return;

            _isGrabbingLeftHand = true;
            onGrabLeftHand?.Invoke();
        }

        /// <summary>
        /// Handler for the cancelled event of the left hand grab input action.
        /// </summary>
        /// <param name="context">Input action event callback context, required for handling input action event.</param>
        private void OnGrabLeftHandCancelled(InputAction.CallbackContext context)
        {
            if (GameManager.gameIsPaused || !_isGrabbingLeftHand)
                return;

            _isGrabbingLeftHand = false;
            onReleaseLeftHand?.Invoke();
        }

        /// <summary>
        /// Handler for the start event of the right hand grab input action.
        /// </summary>
        /// <param name="context">Input action event callback context, required for handling input action event.</param>
        private void OnGrabRightHandStarted(InputAction.CallbackContext context)
        {
            if (GameManager.gameIsPaused)
                return;

            _isGrabbingRightHand = true;
            onGrabRightHand?.Invoke();
        }

        /// <summary>
        /// Handler for the cancelled event of the right hand grab input action.
        /// </summary>
        /// <param name="context">Input action event callback context, required for handling input action event.</param>
        private void OnGrabRightHandCancelled(InputAction.CallbackContext context)
        {
            if (GameManager.gameIsPaused || !_isGrabbingRightHand)
                return;

            _isGrabbingRightHand = false;
            onReleaseRightHand?.Invoke();
        }

        #endregion
    }
}