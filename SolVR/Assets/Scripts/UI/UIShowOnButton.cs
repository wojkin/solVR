using System;
using InputHandlers;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Class responsible for showing and hiding the UIElement on a specified button click in front of the main camera.
    /// </summary>
    [RequireComponent(typeof(UIElement))]
    [RequireComponent(typeof(EventOnButton))]
    public class UIShowOnButton : MonoBehaviour
    {
        private UIElement _uiElement; // UIElement attached to the gameObject

        private EventOnButton _eventOnButton; // EventOnButton responsible for toggling UI on specific button

        [Tooltip("Offset between the player and the UI object.")] [SerializeField]
        private float spawnOffset;

        [Tooltip("The main camera object in front of which the UI object will be displayed.")] [SerializeField]
        private new Transform camera;

        /// <summary>
        /// Initialize fields and add a listener for button input.
        /// </summary>
        private void Start()
        {
            _uiElement = GetComponent<UIElement>();
            _eventOnButton = GetComponent<EventOnButton>();
            _eventOnButton.afterInput.AddListener(VisibilityToggle);
        }

        /// <summary>
        /// Remove listener for button input.
        /// </summary>
        private void OnDisable()
        {
            _eventOnButton.afterInput.RemoveListener(VisibilityToggle);
        }

        /// <summary>
        /// Manages toggling the visibility of the UIElement.
        /// </summary>
        private void VisibilityToggle()
        {
            if (_uiElement.IsVisible)
                Hide();
            else
                Show();
        }

        /// <summary>
        /// Makes the gameObject not visible.
        /// </summary>
        private void Hide()
        {
            _uiElement.Hide();
        }

        /// <summary>
        /// Makes gameObject visible in front of the camera.
        /// </summary>
        private void Show()
        {
            if (camera == null)
                camera = Camera.main.gameObject.transform; // find the main camera object if it's null

            // calculate the position for the gameObject in front of the player
            transform.position = camera.position + camera.forward * spawnOffset;

            _uiElement.Show();
        }
    }
}