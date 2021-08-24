using UnityEngine;

namespace Robots.DevRobot
{
    public class WheelVisual : MonoBehaviour
    {
        [SerializeField] private WheelCollider wheelCollider;
        private const float LerpSpeed = 0.7f;

        void Update()
        {
            wheelCollider.GetWorldPose(out Vector3 targetPosition, out Quaternion targetRotation);
            
            transform.position = Vector3.Lerp(transform.position, targetPosition, LerpSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, LerpSpeed);
        }
    }
}