using UnityEngine;

namespace UI
{
    /// <summary>
    /// Class responsible for showing and hiding the UIElement in front of the main camera.
    /// </summary>
    [RequireComponent(typeof(UIElement))]
    public class UIShowInFrontOfCamera : MonoBehaviour
    {
        #region Serialized Fields

        ///<summary>Offset between the player and the UI object.</summary> 
        [Tooltip("Offset between the player and the UI object.")] [SerializeField]
        private float spawnOffset;

        ///<summary>The main camera object in front of which the UI object will be displayed.</summary> 
        [Tooltip("The main camera object in front of which the UI object will be displayed.")] [SerializeField]
        private new Transform camera;

        #endregion

        #region Variables

        ///<summary>UIElement attached to the gameObject</summary> 
        private UIElement _uiElement;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initialize fields and add a listener for button input.
        /// </summary>
        private void Start()
        {
            _uiElement = GetComponent<UIElement>();
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Manages toggling the visibility of the UIElement.
        /// </summary>
        public void VisibilityToggle()
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
        public void Show()
        {
            if (camera == null)
                camera = Camera.main.gameObject.transform; // find the main camera object if it's null

            // calculate the position for the gameObject in front of the player
            transform.position = camera.position + camera.forward * spawnOffset;

            _uiElement.Show();
        }

        #endregion
    }
}