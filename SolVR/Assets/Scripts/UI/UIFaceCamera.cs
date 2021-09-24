using Managers;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Class for rotating a gameObject to always face the main camera.
    /// </summary>
    [RequireComponent(typeof(UIElement))]
    public class UIFaceCamera : MonoBehaviour
    {
        #region Serialized Fields

        [Tooltip("The camera the gameObject should face.")] [SerializeField]
        private new Transform camera;

        #endregion

        #region Variables

        private UIElement _uiElement; // UIElement attached to the gameObject

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initialize fields.
        /// </summary>
        private void Start()
        {
            _uiElement = GetComponent<UIElement>();
        }

        /// <summary>
        /// Rotates the gameObject to always face the player. Interpolates the rotation in order to make it smoother.
        /// </summary>
        private void Update()
        {
            // not visible or not interactable on pause
            if (!_uiElement.IsVisible || (GameManager.gameIsPaused && !_uiElement.interactableOnPause))
                return;

            if (camera == null)
                camera = Camera.main.gameObject.transform; // find the main camera object if it's null

            // calculate the direction vector from the camera to the gameObject and ignore the vertical difference
            var lookPos = transform.position - camera.position;
            lookPos.y = 0;

            // calculate the rotation at which the gameObject would face the camera
            var rotation = Quaternion.LookRotation(lookPos);

            // interpolate between the gameObject rotation and the desired one
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.unscaledDeltaTime * 100f);
        }

        #endregion
    }
}