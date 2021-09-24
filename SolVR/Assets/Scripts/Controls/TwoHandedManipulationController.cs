using UnityEngine;

namespace Controls
{
    /// <summary>
    /// Class for manipulating an object rotation, translation and scaling based on changing hands positions.
    /// </summary>
    public class TwoHandedManipulationController : MonoBehaviour
    {
        #region Serialized Fields

        public GameObject objectToRotate; // object that is manipulated

        [Tooltip("Multiplier for translation manipulation")] [SerializeField]
        private float translationMultiplier = 1;

        [Tooltip("Multiplier for scaling manipulation")] [SerializeField]
        private float scalingMultiplier = 1;

        [Tooltip("Multiplier for rotation manipulation")] [SerializeField]
        private float rotationMultiplier = 1;

        #endregion

        #region Variables

        // difference between hand positions (vector from left to right hand) from the previous rotation event
        private Vector3 _handsPreviousPositionDifference;

        // center point between hand positions from the previous translation event
        private Vector3 _handsPreviousCenterPosition;

        private float _handsPreviousDistance; // distance between hands from the previous scaling event

        #endregion

        #region Custom Methods

        /// <summary>
        /// Sets initial state for manipulation.
        /// Calculates difference and center point between hand positions.
        /// </summary>
        /// <param name="rightHandPosition">Position of player's right hand.</param>
        /// <param name="leftHandPosition">Position of player's left hand.</param>
        public void OnManipulationStarted(Vector3 rightHandPosition, Vector3 leftHandPosition)
        {
            _handsPreviousPositionDifference = rightHandPosition - leftHandPosition;
            _handsPreviousCenterPosition = (rightHandPosition + leftHandPosition) / 2;
            _handsPreviousDistance = Vector3.Distance(rightHandPosition, leftHandPosition);
        }

        /// <summary>
        /// Rotates specified object based on new hand positions.
        /// </summary>
        /// <param name="rightHandPosition">Position of player's right hand.</param>
        /// <param name="leftHandPosition">Position of player's left hand.</param>
        public void RotateObject(Vector3 rightHandPosition, Vector3 leftHandPosition)
        {
            var handsPreviousPositionDifference = _handsPreviousPositionDifference;
            handsPreviousPositionDifference.y = 0; // ignore rotation on y axis

            var handsPositionDifference = rightHandPosition - leftHandPosition;
            _handsPreviousPositionDifference = handsPositionDifference; // update last hands position difference
            handsPositionDifference.y = 0; // ignore rotation on y axis

            // rotation matrix from last to new hands position difference
            var fromToRotation = Quaternion.FromToRotation(handsPreviousPositionDifference, handsPositionDifference);
            var rotation = objectToRotate.transform.rotation;
            var rotated = rotation * fromToRotation;
            objectToRotate.transform.rotation = Quaternion.SlerpUnclamped(rotation, rotated, rotationMultiplier);
        }

        /// <summary>
        /// Translates specified object based on new hand positions.
        /// </summary>
        /// <param name="rightHandPosition">Position of player's right hand.</param>
        /// <param name="leftHandPosition">Position of player's left hand.</param>
        public void TranslateObject(Vector3 rightHandPosition, Vector3 leftHandPosition)
        {
            var handsCenter = (rightHandPosition + leftHandPosition) / 2;
            var translation = handsCenter - _handsPreviousCenterPosition;
            var position = objectToRotate.transform.position;
            var distance = (position - handsCenter).magnitude; // distance between rotated objects and hands
            // multiplier based on distance from object and translation multiplier
            var multiplier = (1 + distance) * translationMultiplier;
            position += translation * multiplier;
            objectToRotate.transform.position = position;
            _handsPreviousCenterPosition = handsCenter; // update last center between two hand positions
        }

        /// <summary>
        /// Scales specified object based on new hand positions.
        /// </summary>
        /// <param name="rightHandPosition">Position of player's right hand.</param>
        /// <param name="leftHandPosition">Position of player's left hand.</param>
        public void ScaleObject(Vector3 rightHandPosition, Vector3 leftHandPosition)
        {
            var handsDistance = Vector3.Distance(rightHandPosition, leftHandPosition);
            var scale = (handsDistance / _handsPreviousDistance - 1) * scalingMultiplier + 1;
            objectToRotate.transform.localScale *= scale;
            _handsPreviousDistance = handsDistance; // update last distance between two hand positions
        }

        #endregion
    }
}