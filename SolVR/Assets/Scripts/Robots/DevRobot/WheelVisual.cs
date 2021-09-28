using UnityEngine;

namespace Robots.DevRobot
{
    /// <summary>
    /// A class for rotating a wheel mesh to match a wheel colliders rotation and position.
    /// The mesh this script is attached to should be facing forward with no rotation applied.
    /// </summary>
    public class WheelVisual : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>wheel collider which position and rotation will be applied to a mesh</summary>
        [SerializeField] private WheelCollider wheelCollider;

        #endregion

        #region Variables

        /// <summary>factor for the linear interpolation between current and desired transform</summary>
        private const float LerpFactor = 0.7f;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Gets the position and rotation of the wheel collider and applies it to the gameobject this script is
        /// attached to using a lerp function.
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