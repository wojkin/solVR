using Managers;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// A class representing a UI element.
    /// </summary>
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasGroup))]
    public class UIElement : MonoBehaviour
    {
        #region Serialized Fields

        [Tooltip("UI is interactable while the game is paused")] [SerializeField]
        public bool interactableOnPause;

        #endregion

        #region Variables

        private Canvas _canvas; // Canvas attached to the gameObject
        private CanvasGroup _canvasGroup; // Canvas group attached to the gameObject

        // a flag showing whether the canvas is currently visible or hidden and setting canvas visibility
        public bool IsVisible
        {
            get => _canvas.enabled;
            private set => _canvas.enabled = value;
        }

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initialize fields.
        /// </summary>
        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        /// <summary>
        /// Subscribes to all needed events.
        /// </summary>
        private void OnEnable()
        {
            if (!interactableOnPause) // if should be not interactable on pausing the game subscribe to events
            {
                GameManager.OnPause += SetNotInteractive;
                GameManager.OnResume += SetInteractable;
            }
        }

        /// <summary>
        /// Unsubscribes from all previously subscribed events.
        /// </summary>
        private void OnDisable()
        {
            if (!interactableOnPause) // if should be not interactable on pausing the game unsubscribe from events
            {
                GameManager.OnPause -= SetNotInteractive;
                GameManager.OnResume -= SetInteractable;
            }
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Set UI as interactable.
        /// </summary>
        private void SetInteractable()
        {
            _canvasGroup.interactable = true;
        }

        /// <summary>
        /// Set UI as not interactable.
        /// </summary>
        private void SetNotInteractive()
        {
            _canvasGroup.interactable = false;
        }


        /// <summary>
        /// Hides UI canvas (invisible in scene).
        /// </summary>
        public void Hide()
        {
            IsVisible = false;
        }

        /// <summary>
        /// Shows UI canvas (visible in scene).
        /// </summary>
        public void Show()
        {
            IsVisible = true;
        }

        #endregion
    }
}