using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Controls.Interactions
{
    /// <summary>
    /// Manager for XR Interactor responsible for UI intractability. 
    /// </summary>
    [RequireComponent(typeof(XRRayInteractor))]
    public class XRInteractorUIInteractionManager : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>Component that visualize <see cref="_xrRayInteractor"/> as line.</summary>
        [Tooltip("Component that visualize XR Interactor as line")] [SerializeField]
        private XRInteractorLineVisual xrInteractorLineVisual;

        #endregion

        #region Variables

        /// <summary>Reference to XR Ray interactor component on this gameObject.</summary>
        private XRRayInteractor _xrRayInteractor;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initialize fields.
        /// </summary>
        private void Start()
        {
            _xrRayInteractor = GetComponent<XRRayInteractor>();
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Disables interaction with UI and hides the pointer.
        /// </summary>
        public void EnableUIInteraction()
        {
            _xrRayInteractor.enableUIInteraction = true;
            if (xrInteractorLineVisual != null)
                xrInteractorLineVisual.enabled = true;
        }

        /// <summary>
        /// Disables interaction with UI and hides the pointer.
        /// </summary>
        public void DisableUIInteraction()
        {
            _xrRayInteractor.enableUIInteraction = false;
            if (xrInteractorLineVisual == null) return;

            // disable line and reticle for interactor
            xrInteractorLineVisual.enabled = false;
            if (xrInteractorLineVisual.reticle != null)
                xrInteractorLineVisual.reticle.SetActive(false);
        }

        #endregion
    }
}