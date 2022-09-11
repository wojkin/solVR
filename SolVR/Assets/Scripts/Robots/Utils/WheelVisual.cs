using UnityEngine;

namespace Robots.Utils
{
    /// <summary>
    /// A class for rotating a wheel mesh to match a wheel colliders rotation and position.
    /// </summary>
    /// <remarks>The mesh this script is attached to should be facing forward with no rotation applied.</remarks>
    public class WheelVisual : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>Wheel collider which position and rotation will be applied to a mesh.</summary>
        [SerializeField] private WheelCollider wheelCollider;

        #endregion

        #region Variables

        /// <summary>Factor for the linear interpolation between current and desired transform.</summary>
        private const float LerpFactor = 0.7f;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Apply wheel collier's position and rotation to the object this script is attached to.
        /// </summary>
        void Update()
        {
            // get position and rotation
            wheelCollider.GetWorldPose(out var targetPosition, out var targetRotation);

            // apply position and rotation using lerp
            transform.position = Vector3.Lerp(transform.position, targetPosition, LerpFactor);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, LerpFactor);
        }

        #endregion
    }
}