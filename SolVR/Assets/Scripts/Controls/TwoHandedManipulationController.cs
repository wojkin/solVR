using UnityEngine;

namespace Controls
{
    /// <summary>
    /// Class for manipulating an object rotation, translation and scaling based on changing hands positions.
    /// </summary>
    public class TwoHandedManipulationController : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>Object that is manipulated.</summary>
        public GameObject objectToManipulate;

        /// <summary>Multiplier for translation manipulation.</summary>
        [Tooltip("Multiplier for translation manipulation.")] [SerializeField]
        private float translationMultiplier = 1;

        /// <summary>Multiplier for scaling manipulation.</summary>
        [Tooltip("Multiplier for scaling manipulation.")] [SerializeField]
        private float scalingMultiplier = 1;

        /// <summary>Multiplier for rotation manipulation.</summary>
        [Tooltip("Multiplier for rotation manipulation.")] [SerializeField]
        private float rotationMultiplier = 1;

        #endregion

        #region Variables

        /// <summary>
        /// Difference between hand positions (vector from left to right hand) from the previous rotation event.
        /// </summary>
        private Vector3 _handsPreviousPositionDifference;

        /// <summary>Center point between hand positions from the previous translation event.</summary>
        private Vector3 _handsPreviousCenterPosition;

        /// <summary>Distance between hands from the previous scaling event.</summary>
        private float _handsPreviousDistance;

        #endregion

        #region Custom Methods

        /// <summary>
        /// Sets initial state for manipulation.
        /// </summary>
        /// <param name="rightHandPosition">Position of player's right hand.</param>
        /// <param name="leftHandPosition">Position of player's left hand.</param>
        public void OnManipulationStarted(Vector3 rightHandPosition, Vector3 leftHandPosition)
        {
            // calculate difference and center point between hand positions.
            _handsPreviousPositionDifference = rightHandPosition - leftHandPosition;
            _handsPreviousCenterPosition = (rightHandPosition + leftHandPosition) / 2;
            _handsPreviousDistance = Vector3.Distance(rightHandPosition, leftHandPosition);
            TranslateManipulatedObjectToCenterPointOfChildren();
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
            // center point between right and left hand positions
            var center = (rightHandPosition + leftHandPosition) / 2;
            RotateAroundByQuaternion(center, fromToRotation);
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
            var position = objectToManipulate.transform.position;
            var distance = (position - handsCenter).magnitude; // distance between rotated objects and hands
            // multiplier based on distance from object and translation multiplier
            var multiplier = (1 + distance) * translationMultiplier;
            position += translation * multiplier;
            objectToManipulate.transform.position = position;
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
            objectToManipulate.transform.localScale *= scale;
            _handsPreviousDistance = handsDistance; // update last distance between two hand positions
        }

        /// <summary>
        /// Rotates specified object around a pivot by a rotation quaternion.
        /// </summary>
        /// <param name="pivot">Point to rotate around.</param>
        /// <param name="rotation">Quaternion by which object will be rotated.</param>
        private void RotateAroundByQuaternion(Vector3 pivot, Quaternion rotation)
        {
            var objectRotation = objectToManipulate.transform.rotation;
            var objectPosition = objectToManipulate.transform.position;

            // rotated object's rotation, without scaling with the multiplier.
            var desiredRotation = objectRotation * rotation;
            // rotation scaled with the multiplier.
            var rotationMultiplied = Quaternion
                .SlerpUnclamped(objectRotation, desiredRotation, rotationMultiplier);

            // position, without scaling with the multiplier, of object after rotation
            var desiredPosition = rotation * (objectPosition - pivot) + pivot;
            // vector delta which of with position should be moved, without scaling with multiplier.
            var positionDelta = desiredPosition - objectPosition;

            objectToManipulate.transform.rotation = rotationMultiplied;
            // moving object's position by scaled delta position vector
            objectToManipulate.transform.position += positionDelta * rotationMultiplier;
        }

        /// <summary>
        /// Gets the middle of positions of all <see cref="parent"/>'s children.
        /// </summary>
        /// <param name="parent">Transform of a parent which middle point is calculated.</param>
        /// <returns></returns>
        private static Vector3 GetCenterPointOfChildren(Transform parent)
        {
            if (parent.childCount == 0) return parent.position;
            var totalPoint = Vector3.zero;
            for (var id = 0; id < parent.childCount; id++)
            {
                var childPosition = parent.GetChild(id).transform.position;
                totalPoint.x += childPosition.x;
                totalPoint.y += childPosition.y;
                totalPoint.z += childPosition.z;
            }

            return totalPoint / parent.childCount;
        }

        /// <summary>
        /// Changes position of all  <see cref="parent"/>'s children by given <see cref="vector"/>.
        /// </summary>
        /// <param name="parent">Transform of a parent which children's position is modified.</param>
        /// <param name="vector">Vector by which objects are transformed.</param>
        private static void TranslateChildrenByVector(Transform parent, Vector3 vector)
        {
            for (var id = 0; id < parent.childCount; id++) parent.GetChild(id).transform.position += vector;
        }

        /// <summary>
        /// Translates <see cref="objectToManipulate"/> on position which is in the middle of positions of all its
        /// children.
        /// </summary>
        private void TranslateManipulatedObjectToCenterPointOfChildren()
        {
            var parent = objectToManipulate.transform;
            var center = GetCenterPointOfChildren(parent);
            var transformVector = parent.position - center;
            parent.position = center;
            TranslateChildrenByVector(parent, transformVector);
        }

        #endregion
    }
}