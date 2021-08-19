using UnityEngine;

namespace Controls
{
    /// <summary>
    /// Class for manipulating an object rotation based on changing hands positions.
    /// </summary>
    public class TwoHandedManipulationController : MonoBehaviour
    {
        // difference between hands positions when manipulation started, vector from left to right hand
        private Vector3 _handsInitialPositionDifference; 
        
        public GameObject objectToRotate; // object that is manipulated
        
        private Quaternion _initialRotation; // object rotation when manipulation started
        
        private Vector3 _initialPosition; // object position when manipulation started

        /// <summary>
        /// Sets initial state for manipulation.
        /// Sets position and rotation of an object and calculates difference between hands positions.
        /// </summary>
        /// <param name="rightHandPosition">Position of player's right hand.</param>
        /// <param name="leftHandPosition">Position of player's left hand.</param>
        public void OnManipulationStarted(Vector3 rightHandPosition, Vector3 leftHandPosition)
        {
            _handsInitialPositionDifference = rightHandPosition - leftHandPosition;
            _initialRotation = objectToRotate.transform.rotation;
            _initialPosition = objectToRotate.transform.position;
        }
        
        /// <summary>
        /// Rotates specified object based on new hands position.
        /// </summary>
        /// <param name="rightHandPosition">Position of player's right hand.</param>
        /// <param name="leftHandPosition">Position of player's left hand.</param>
        public void RotateObject(Vector3 rightHandPosition, Vector3 leftHandPosition)
        {
            var handsPositionDifference = rightHandPosition - leftHandPosition;
            handsPositionDifference.y = 0; // ignore rotation on y axis
            
            var handsInitialPositionDifference = _handsInitialPositionDifference;
            handsInitialPositionDifference.y = 0; // ignore rotation on y axis
            
            // rotation matrix from initial to new hands position difference
            var rotation = Quaternion.FromToRotation(handsInitialPositionDifference, handsPositionDifference);
            // center point between right and left hands positions
            var center = (rightHandPosition + leftHandPosition) / 2;
            RotateAroundByQuaternion(center, rotation);
        }
        
        /// <summary>
        /// Rotates specified object around a pivot by a rotation quaternion.
        /// </summary>
        /// <param name="pivot">Point to rotate around.</param>
        /// <param name="rotation">Quaternion by which object will be rotated.</param>
        private void RotateAroundByQuaternion(Vector3 pivot, Quaternion rotation)
        {
            objectToRotate.transform.rotation = _initialRotation * rotation;
            objectToRotate.transform.position = rotation * (_initialPosition - pivot) + pivot;
        }
        
    }
}