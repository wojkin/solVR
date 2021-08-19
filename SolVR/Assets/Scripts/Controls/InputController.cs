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
        private PlayerInputActions _playerInputActions; // player input action with manipulation action

        private XRIDefaultInputActions _xriInputActions; // xri input action with hands positions

        // event invoked on TwoHandedManipulation input action started, first parameter is right hand position and
        // the second one is left hand position
        public UnityEvent<Vector3, Vector3> onTwoHandedManipulationStarted;

        // event invoked from start to cancel of TwoHandedManipulation input action, first parameter is right hand
        // position and the second is left hand position
        public UnityEvent<Vector3, Vector3> onTwoHandedManipulating;

        // flag showing if two handed manipulation is in progress, set to true after input action is started, until
        // it's canceled
        private bool _isManipulated;
        
        /// <summary>
        /// Initialize fields.
        /// </summary>
        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();
            _xriInputActions = new XRIDefaultInputActions();
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
            GameManager.OnPause += OnPause;
        }

        /// <summary>
        /// Handler for after pause event.
        /// </summary>
        private void OnPause() => _isManipulated = false;

        /// <summary>
        /// Handler for started event of TwoHandedManipulation input action.
        /// Invokes onTwoHandedManipulationStarted event with positions of right and left hands as parameters.
        /// </summary>
        /// <param name="context">Input action event callback context, require by input action event</param>
        private void OnTwoHandedManipulationStarted(InputAction.CallbackContext context)
        {
            if (GameManager.gameIsPaused)
                return;
            var rightHandPosition = _xriInputActions.XRIRightHand.Position.ReadValue<Vector3>();
            var leftHandPosition = _xriInputActions.XRILeftHand.Position.ReadValue<Vector3>();
            _isManipulated = true;
            onTwoHandedManipulationStarted?.Invoke(rightHandPosition, leftHandPosition);
        }

        /// <summary>
        /// Handler for canceled event of TwoHandedManipulation input action.
        /// Sets manipulating flag to false.
        /// </summary>
        /// <param name="context">Input action event callback context, require by input action event</param>
        private void OnTwoHandedManipulationCanceled(InputAction.CallbackContext context) => _isManipulated = false;

        /// <summary>
        /// Invokes onTwoHandedManipulating event, with positions of right and left hands as parameters, as long as two
        /// handed manipulation is in progress.
        /// </summary>
        private void Update()
        {
            if (_isManipulated)
            {
                var rightHandPosition = _xriInputActions.XRIRightHand.Position.ReadValue<Vector3>();
                var leftHandPosition = _xriInputActions.XRILeftHand.Position.ReadValue<Vector3>();
                onTwoHandedManipulating?.Invoke(rightHandPosition, leftHandPosition);
            }
        }

        /// <summary>
        /// Disables input actions and unsubscribes from all needed events.
        /// </summary>
        private void OnDisable()
        {
            GameManager.OnPause -= OnPause;
            _playerInputActions.Player.TwoHandedManipulation.started -= OnTwoHandedManipulationStarted;
            _playerInputActions.Player.TwoHandedManipulation.canceled -= OnTwoHandedManipulationCanceled;
            _xriInputActions.XRIRightHand.Position.Disable();
            _xriInputActions.XRILeftHand.Position.Disable();
            _playerInputActions.Player.TwoHandedManipulation.Disable();
        }
    }
}