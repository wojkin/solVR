using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Class responsible for showing and hiding the UIElement on a specified button click in front of the main camera.
    /// </summary>
    [RequireComponent(typeof(UIElement))]
    public class UIShowOnButton : MonoBehaviour
    {
        private UIElement _uiElement; // UIElement attached to the gameObject

        [Tooltip("Oculus controller button used to show and hide the UIElement.")]
        [SerializeField] 
        public OVRInput.Button button;
        
        [Tooltip("Offset between the player and the UI object.")] 
        [SerializeField]
        private float spawnOffset;

        [Tooltip("The main camera object in front of which the UI object will be displayed.")] 
        [SerializeField]
        private Transform camera;

        /// <summary>
        /// Initialize fields.
        /// </summary>
        private void Start()
        {
            _uiElement = GetComponent<UIElement>();
        }
        
        /// <summary>
        /// Makes the gameObject face the camera when it's visible and manages hiding and showing the gameObject on a
        /// button press.
        /// </summary>
        private void Update()
        {
            // if the button is pressed show or hide the gameObject
            if (OVRInput.GetUp(button))
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