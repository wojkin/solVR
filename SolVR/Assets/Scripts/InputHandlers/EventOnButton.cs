using UnityEngine;
using UnityEngine.Events;

namespace InputHandlers
{
    /// <summary>
    /// Class calling events based on a button input.
    /// </summary>
    public class EventOnButton : MonoBehaviour
    {
        [Tooltip("Oculus controller button used to call events.")]
        public UnityEvent afterInput;
        
        [Tooltip("Oculus controller button used to call events.")]
        [SerializeField] 
        private OVRInput.Button button;

        /// <summary>
        /// Invoke event listeners on a specified button press.
        /// </summary>
        private void Update()
        {
            // if the button is pressed invoke event
            if (OVRInput.GetUp(button))
                afterInput?.Invoke();
        }
    }
}