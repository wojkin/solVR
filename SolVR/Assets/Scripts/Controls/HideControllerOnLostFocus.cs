using UnityEngine;

namespace Controls
{
    /// <summary>
    /// Class responsible for hiding and showing controllers on input focus events.
    /// </summary>
    public class HideControllerOnLostFocus : MonoBehaviour
    {
        /// <summary>Game object with left controller</summary>
        [SerializeField]
        private GameObject leftController;
        
        /// <summary>Game object with right controller.</summary>
        [SerializeField]
        private GameObject rightController;

        /// <summary>
        /// Subscribes to needed events.
        /// </summary>
        private void OnEnable()
        {
            OVRManager.InputFocusAcquired += ShowControllers;
            OVRManager.InputFocusLost += HideControllers;
        }

        /// <summary>
        /// Unsubscribes from previously subscribed events.
        /// </summary>
        private void OnDisable()
        {
            OVRManager.InputFocusAcquired -= ShowControllers;
            OVRManager.InputFocusLost -= HideControllers;
        }

        /// <summary>
        /// Sets both controllers to not active.
        /// </summary>
        private void HideControllers()
        {
            leftController.SetActive(false);
            rightController.SetActive(false);
        }

        /// <summary>
        /// Shows both controllers.
        /// </summary>
        private void ShowControllers()
        {
            leftController.SetActive(true);
            rightController.SetActive(true);
        }
    }
}
